using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketEnter : MonoBehaviour
{
    public Glasses glassesManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Glasses")
        {
            glassesManager.turnOn = true;
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Glasses")
        {
            glassesManager.turnOn = false;
        }
    }
}
