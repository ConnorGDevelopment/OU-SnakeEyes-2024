using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunFire : MonoBehaviour
{








    //OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);                          (For finding the trigger buttons)
    //OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);



    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerShot;
    public bool allowTriggerHold;
    int bulletsleft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;


    //controller triggers

    public InputActionProperty triggerAction;


    //reference
    public Camera Camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;








    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}