using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR;

public class ControllerTriggerFireRight : MonoBehaviour
{
    public GameObject Revolver;
    public GameObject RevolverSocket;        //The object that detects if the gun is in hand
    public GameObject BulletPrefab;            //Bullet prefab
    public Transform Firepoint;             //Where the bullet will be fired from
    public float GunDetect = 0.5f;      // Variable for the range detection of the gun being in hand
    private bool _inHand;     //Initializing the value to false, can't start the game with gun in hand :/
    private ControllerInputs inputData;
    

    private bool _rightTriggerPressed = false;

    void Start()
    {

        inputData = GetComponent<ControllerInputs>();   //Getting the data inputs from the controller
        _inHand = false;        //setting the InHand to false


    }

    void Update()
    {



        if (Vector3.Distance(Revolver.transform.position, RevolverSocket.transform.position) <= GunDetect)  //Checking to see if the gun is in the collider
        {
            _inHand = true;
        }
        else
        {
            _inHand = false;    //if the gun is not in the collider then checks in hand as false
        }




        if (_inHand == true)             //Checking to see if the right gun is in the hand
        {
            if (inputData._rightController.isValid)     //Checking to see if the controller data is valid
            {
                if (inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue))  //Checking for data values
                {
                    if (rightTriggerValue > 0.1f && !_rightTriggerPressed)          //Taking trigger values and checking values to see if the trigger is pressed or not
                    {
                        _rightTriggerPressed = true;
                        FireBullet();
                    }
                    else if (rightTriggerValue <= 0.1f && _rightTriggerPressed)
                    {
                        _rightTriggerPressed = false;
                        Debug.Log("Right Trigger Released");
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
            bulletRb.velocity = Firepoint.forward * 150f;
        }


    }



}
