using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticTest : MonoBehaviour
{

    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;
    public uint channel = 1;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vibrate();
    }

    public void Vibrate()
    {
        if(timer >= 1f)
        {
            _rightController.SendHapticImpulse(channel, 1, 1);
            timer = 0;
        }
        
    }

}
