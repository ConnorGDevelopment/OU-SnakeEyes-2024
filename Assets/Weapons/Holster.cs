using UnityEngine;
using Weapons;

public class HolsterScript : MonoBehaviour
{
    public GameObject leftHolster;         // Reference to the left holster
    public GameObject rightHolster;        // Reference to the right holster
    public GameObject revolverPrefab;      // The prefab to instantiate
    private GameObject currentRevolver;    // The currently instantiated revolver
    private Revolver bullet_count;  //Reference to the Revolver script
    public AudioClip GunDrop;       //Gun dropping sfx
    private AudioSource _audioSource;  //reference to the audio source (question do I even need to keep setting it like this?)




    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void SpawnRevolver()
    {
        // Check which hand the revolver belongs to
        if (CompareTag("LeftHand")) //Checking left hand
        {
            bullet_count = GetComponent<Revolver>();

            bullet_count.bulletCount = 6;         //Resetting the bullet count of the revolvers back to 6 (temporary, will be later changed to a variable that references the scriptable object which holds the number of shots the player has)



            currentRevolver = Instantiate(revolverPrefab, leftHolster.transform.position, leftHolster.transform.rotation);  //Creating the new revolver and setting it to the "current revolver" in play
            Debug.Log("Spawned revolver in left holster.");   //debugging
        }
        else if (CompareTag("RightHand")) //Checking right hand
        {

            bullet_count = GetComponent<Revolver>();

            bullet_count.bulletCount = 6;      //Resetting bullet count


            currentRevolver = Instantiate(revolverPrefab, rightHolster.transform.position, rightHolster.transform.rotation); //Creating the new revolver and setting it to the "current revolver" in play
            Debug.Log("Spawned revolver in right holster.");  //debugging
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

            SpawnRevolver();     //Spawning revolvers before destroying the old ones, this keeps the data the exact same from the previous revolvers to prevent issues from occuring

            _audioSource.PlayOneShot(GunDrop, 1f);   //Playing audio for the revolvers hitting the ground

            Destroy(gameObject, GunDrop.length);   //Destroying the revolver with this script and touching the object with the tag GunDestroy.
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

    
}