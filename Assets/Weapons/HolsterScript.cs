using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

public class HolsterScript : MonoBehaviour
{
    public GameObject RevolverPrefab;  // Prefab for instantiating the revolver
    public Transform HolsterLeft;
    public Transform HolsterRight;
    public bool RightHandSpawn = false;
    public bool LeftHandSpawn = false;

    private GameObject rightHandRevolver;  // Reference to the revolver currently in right hand
    private GameObject leftHandRevolver;   // Reference to the revolver currently in left hand

    private void Start()
    {
        HolsterLeft = GameObject.Find("Holster left transform").transform;
        HolsterRight = GameObject.Find("Holster right transform").transform;
    }

    private void Update()
    {
        // Right hand revolver spawning logic
        if (RightHandSpawn == true)
        {
            Debug.Log("RightHandSpawn is true. Spawning right revolver.");

            // Find any existing right hand revolver and destroy it
            GameObject existingRightRevolver = GameObject.FindWithTag("RightHand");
            if (existingRightRevolver != null)
            {
                Debug.Log("Found existing right hand revolver. Destroying it before spawning a new one.");
                Destroy(existingRightRevolver);
            }

            // Spawn a new revolver in the right holster
            rightHandRevolver = Instantiate(RevolverPrefab, HolsterRight.position, HolsterRight.rotation);
            rightHandRevolver.tag = "RightHand";  // Assign correct tag

            Rigidbody rb = rightHandRevolver.GetComponent<Rigidbody>();
            if(rb != null)
            {
                
            }
            RightHandSpawn = false;
        }

        // Left hand revolver spawning logic
        if (LeftHandSpawn == true)
        {
            Debug.Log("LeftHandSpawn is true. Spawning left revolver.");

            // Find any existing left hand revolver and destroy it
            GameObject existingLeftRevolver = GameObject.FindWithTag("LeftHand");
            if (existingLeftRevolver != null)
            {
                Debug.Log("Found existing left hand revolver. Destroying it before spawning a new one.");
                Destroy(existingLeftRevolver);
            }

            // Spawn a new revolver in the left holster
            leftHandRevolver = Instantiate(RevolverPrefab, HolsterLeft.position, HolsterLeft.rotation);
            leftHandRevolver.tag = "LeftHand";  // Assign correct tag
            LeftHandSpawn = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name + " Tag: " + other.gameObject.tag);

        
        if (this.gameObject.CompareTag("LeftHand") || this.gameObject.CompareTag("RightHand"))
        {
            if (other.CompareTag("GunDestroy"))
            {
                Debug.Log("Collided with the GunDestroyer"); //debugging to see if the revolver collided with the gun destroyer

                if (this.gameObject.CompareTag("LeftHand"))
                {
                    Debug.Log("Destroying left revolver, setting LeftHandSpawn to true");
                    Destroy(this.gameObject, 0.1f);   //Destroys the game object after a brief delay so it can ru nthe rest of the code
                    LeftHandSpawn = true;
                }
                else if (this.gameObject.CompareTag("RightHand"))
                {
                    Debug.Log("Destroying right hand revolver, setting RightHandSpawn to true");
                    Destroy(this.gameObject, 0.1f);  //Destroys the game object after a brief delay so it can run the rest of the code
                    RightHandSpawn = true;
                }
                else
                {
                    Debug.Log("Gun touching the collider doesn't have the correct tag");
                }
            }
        }
    }
}
