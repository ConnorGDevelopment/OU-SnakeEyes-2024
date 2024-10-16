using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerTriggerFire : MonoBehaviour
{
    private ControllerInputs inputData;
    public GameObject Revolver;
    public GameObject BulletPrefab;
    public Transform Firepoint;
    

    private bool InHand = false;
    private bool triggerPressed = false;
    private InputDevice activeController;  // Tracks which controller is holding the gun

    void Start()
    {
        inputData = GetComponent<ControllerInputs>();    // Getting the controller input data
        InHand = false;  // Ensure gun starts out of hand
    }

    void Update()
    {
        // If the gun is in hand, check which controller is holding it and handle firing logic
        if (InHand && activeController != null && activeController.isValid)
        {
            if (activeController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                if (triggerValue > 0.1f && !triggerPressed)
                {
                    triggerPressed = true;
                    FireBullet();
                }
                else if (triggerValue <= 0.1f && triggerPressed)
                {
                    triggerPressed = false;
                    Debug.Log("Trigger Released");
                }
            }
        }
        else
        {
            // If not in hand, reset trigger press state to avoid accidental fire
            triggerPressed = false;
        }
    }

    private void FireBullet()
    {
        // Instantiate the bullet prefab at the fire point with the correct orientation
        GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = Firepoint.forward * 60f;  // Apply force to the bullet
        }

        Debug.Log("Bullet Fired!");
    }

    // Detect when the revolver enters the collider for the hand (i.e., gun is picked up)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            activeController = inputData._leftController;
            InHand = true;
            Debug.Log("Revolver picked up by Left Hand");
        }
        else if (other.CompareTag("RightHand"))
        {
            activeController = inputData._rightController;
            InHand = true;
            Debug.Log("Revolver picked up by Right Hand");
        }
    }

    // Detect when the revolver exits the collider for the hand (i.e., gun is dropped)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            InHand = false;
            activeController = new InputDevice();  // Clear active controller when gun is no longer in hand
            Debug.Log("Revolver dropped");
        }
    }
}
