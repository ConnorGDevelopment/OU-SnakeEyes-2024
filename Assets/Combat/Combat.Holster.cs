using UnityEngine;

namespace Combat
{
    public class Holster : MonoBehaviour
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
    }
}