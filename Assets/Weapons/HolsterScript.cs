using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HolsterScript : MonoBehaviour
{


    public GameObject Revolver;
    public Transform HolsterLeft;
    public Transform HolsterRight;
    public BoxCollider GunCollider;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GunDestroy"))
        {
            if (Revolver.tag == "LeftHand")
            {
                GameObject OriginalRevolver = GameObject.FindWithTag("LeftHand");

                Destroy(OriginalRevolver);

                Instantiate(Revolver, HolsterLeft.position, HolsterLeft.rotation);

            }




            if (Revolver.tag == "RightHand")
            {
                GameObject OriginalRevolver = GameObject.FindWithTag("RightHand");
                
                Destroy(OriginalRevolver);

                Instantiate(Revolver, HolsterRight.position, HolsterRight.rotation);
            }
        }
    }


}
