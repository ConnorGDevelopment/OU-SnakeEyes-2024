using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HolsterScript : MonoBehaviour
{
    public GameObject RevolverPrefab;  // Prefab for instantiating the revolver
    public Transform HolsterLeft;
    public Transform HolsterRight;
    public bool RightHandSpawn = false;
    public bool LeftHandSpawn = false;

    private GameObject rightHandRevolver;  // Reference to the revolver currently in right hand
    private GameObject leftHandRevolver;   // Reference to the revolver currently in left hand

    private void Update()
    {
        if (RightHandSpawn)
        {
            if (rightHandRevolver != null)
            {
                Destroy(rightHandRevolver);
            }

            rightHandRevolver = Instantiate(RevolverPrefab, HolsterRight.position, HolsterRight.rotation);
            rightHandRevolver.tag = "RightHand";  // Ensure correct tag is assigned
            RightHandSpawn = false;
        }

        if (LeftHandSpawn)
        {
            if (leftHandRevolver != null)
            {
                Destroy(leftHandRevolver);
            }

            leftHandRevolver = Instantiate(RevolverPrefab, HolsterLeft.position, HolsterLeft.rotation);
            leftHandRevolver.tag = "LeftHand";  // Ensure correct tag is assigned
            LeftHandSpawn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GunDestroy"))
        {
            if (other.gameObject.CompareTag("LeftHand"))
            {
                LeftHandSpawn = true;
            }
            else if (other.gameObject.CompareTag("RightHand"))
            {
                RightHandSpawn = true;
            }
        }
    }
}
