using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public Transform target;         //very simple enemy script to track where the player is and go to that location
    public float speed;



    private void Start()
    {
        speed = Random.Range(0.008f, 0.05f);
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }


}
