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
            if (Revolver.tag == "LeftRevolver")
            {
                GameObject OriginalRevolver = GameObject.FindWithTag("LeftRevolver");

                Destroy(OriginalRevolver);

                Instantiate(Revolver, HolsterLeft.position, HolsterLeft.rotation);

            }




            if (Revolver.tag == "RightRevolver")
            {
                GameObject OriginalRevolver = GameObject.FindWithTag("RightRevolver");
                
                Destroy(OriginalRevolver);

                Instantiate(Revolver, HolsterRight.position, HolsterRight.rotation);
            }
        }
    }


}