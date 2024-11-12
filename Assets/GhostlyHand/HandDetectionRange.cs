using UnityEngine;

public class HandDetectionRange : MonoBehaviour
{
    private HolsterScript holsterScript; // Reference to the HolsterScript

    private void Start()
    {
        // Get the HolsterScript component on the same GameObject
        holsterScript = GetComponent<HolsterScript>();
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the revolver
        if (other.CompareTag("LeftHand") | other.CompareTag("RightHand"))
        {
            // Call the method to enable gravity on the revolver
            holsterScript.EnableGravity();
        }
    }
}
