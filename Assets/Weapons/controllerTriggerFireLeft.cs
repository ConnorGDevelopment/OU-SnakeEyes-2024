using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class ControllerTriggerFireLeft : MonoBehaviour
{
    private ControllerInputs inputData;
    public GameObject Revolver;
    public GameObject RevolverSocket;        //The object that detects if the gun is in hand
    public GameObject BulletPrefab;
    public Transform Firepoint;
    public float GunDetect = 0.5f;      // Variable for the range detection of the gun being in hand
    private bool InHand;     //Initializing the value to false, can't start the game with gun in hand :(
    private bool _leftTriggerPressed = false;

    void Start()
    {
        inputData = GetComponent<ControllerInputs>();    //Getting the data inputs from the controller
        InHand = false;        //setting the InHand to false which means the gun doesn't start in the hand
    }

    void Update()
    {


        if (Vector3.Distance(Revolver.transform.position, RevolverSocket.transform.position) <= GunDetect)  //Checking to see if the gun is in the collider
        {
            InHand = true;
        }
        else
        {
            InHand = false;
        }


        if (InHand == true)    //checking to see if the left gun is in the left hand
        {
            // Check left controller trigger value
            if (inputData._leftController.isValid)
            {
                if (inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue))
                {
                    if (leftTriggerValue > 0.1f && !_leftTriggerPressed)
                    {
                        _leftTriggerPressed = true;
                        FireBullet();
                    }
                    else if (leftTriggerValue <= 0.1f && _leftTriggerPressed)
                    {
                        _leftTriggerPressed = false;
                        Debug.Log("Left Trigger Released");
                    }
                }
            }
        }




        

    }

    private void FireBullet()
    {

        GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Firepoint.forward * 60f;
        }


    }
}