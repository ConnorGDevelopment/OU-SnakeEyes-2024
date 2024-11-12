using UnityEngine;

public class UpgradeCubePickup : MonoBehaviour
{
    public SpawningScript spawningScript; // Reference to the SpawningScript

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand")) //Looking at the tag of the hand detection
        {
            spawningScript.OnUpgradePickedUp();
        }
    }
}