using UnityEngine;

public class HolsterScript : MonoBehaviour
{
    public GameObject leftHolster;         // Reference to the left holster
    public GameObject rightHolster;        // Reference to the right holster
    public GameObject currentGun;      // The prefab to instantiate


    private void SpawnRevolver()
    {
        // Check which hand the revolver belongs to
        if (CompareTag("LeftHand")) //Checking left hand
        {


            currentGun = Instantiate(currentGun, leftHolster.transform.position, leftHolster.transform.rotation);  //Creating the new revolver and setting it to the "current revolver" in play
            Debug.Log("Spawned revolver in left holster.");   //debugging
        }
        else if (CompareTag("RightHand")) //Checking right hand
        {

            currentGun = Instantiate(currentGun, rightHolster.transform.position, rightHolster.transform.rotation); //Creating the new revolver and setting it to the "current revolver" in play
            Debug.Log("Spawned revolver in right holster.");  //debugging
        }

        if (currentGun.TryGetComponent(out Rigidbody rb))
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
        if (currentGun != null)
        {
            // Logic for picking up the revolver
            currentGun.transform.SetParent(playerHand.transform); // Attach to the player's hand
            currentGun.transform.localPosition = Vector3.zero; // Reset position
            currentGun.transform.localRotation = Quaternion.identity; // Reset rotation

        }
    }

    // Method to handle dropping the revolver
    //public void DropRevolver()
    //{
    //    if (currentRevolver = null)
    //    {
    //        // Detach from the player's hand
    //        currentRevolver.transform.SetParent(null);

    //        // Get the Rigidbody and re-enable gravity
    //        Rigidbody rb = currentRevolver.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.useGravity = true; // Enable gravity when dropped
    //            Debug.Log("Revolver dropped, gravity enabled.");
    //        }
    //    }
    //}

    // Method to turn off gravity when the revolver exits the detection range
    //public void EnableGravity()
    //{
    //    if (currentRevolver != null)
    //    {
    //        Rigidbody rb = currentRevolver.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.useGravity = true; // Enable gravity when the revolver exits the range
    //            Debug.Log("Revolver exited range, gravity enabled.");
    //        }
    //    }
    //}
}