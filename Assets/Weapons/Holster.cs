using UnityEngine;
using Weapons;

public class HolsterScript : MonoBehaviour
{
    public GameObject leftHolster;         // Reference to the left holster
    public GameObject rightHolster;        // Reference to the right holster
    public GameObject revolverPrefab;      // The prefab to instantiate
    private GameObject currentRevolver;    // The currently instantiated revolver
    private Revolver bullet_count;
    

    private void SpawnRevolver()
    {
        // Check which hand the revolver belongs to
        if (CompareTag("LeftHand"))
        {
            bullet_count = GetComponent<Revolver>();

            bullet_count.bulletCount = 6;

            

            currentRevolver = Instantiate(revolverPrefab, leftHolster.transform.position, leftHolster.transform.rotation);
            Debug.Log("Spawned revolver in left holster.");
        }
        else if (CompareTag("RightHand"))
        {

            bullet_count = GetComponent<Revolver>();

            bullet_count.bulletCount = 6;
            currentRevolver = Instantiate(revolverPrefab, rightHolster.transform.position, rightHolster.transform.rotation);
            Debug.Log("Spawned revolver in right holster.");
        }

        Rigidbody rb = currentRevolver.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Ensure the revolver stays in the holster
            Debug.Log("Gravity set to false in holster.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the GunDestroy object
        if (other.CompareTag("GunDestroy"))
        {
            // Log destruction
            Debug.Log("GunDestroy triggered!");
            SpawnRevolver();
            Destroy(gameObject);
        }
    }

    // Method to handle the pickup interaction
    public void PickupRevolver(GameObject playerHand)
    {
        if (currentRevolver != null)
        {
            // Logic for picking up the revolver
            currentRevolver.transform.SetParent(playerHand.transform); // Attach to the player's hand
            currentRevolver.transform.localPosition = Vector3.zero; // Reset position
            currentRevolver.transform.localRotation = Quaternion.identity; // Reset rotation

        }
    }

    // Method to handle dropping the revolver
    public void DropRevolver()
    {
        if (currentRevolver = null)
        {
            // Detach from the player's hand
            currentRevolver.transform.SetParent(null);

            // Get the Rigidbody and re-enable gravity
            Rigidbody rb = currentRevolver.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Enable gravity when dropped
                Debug.Log("Revolver dropped, gravity enabled.");
            }
        }
    }

    // Method to turn off gravity when the revolver exits the detection range
    public void EnableGravity()
    {
        if (currentRevolver != null)
        {
            Rigidbody rb = currentRevolver.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Enable gravity when the revolver exits the range
                Debug.Log("Revolver exited range, gravity enabled.");
            }
        }
    }
}
