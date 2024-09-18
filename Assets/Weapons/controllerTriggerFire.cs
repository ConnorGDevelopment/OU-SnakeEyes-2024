using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerTriggerFire : MonoBehaviour
{
    private ControllerInputs inputData;

    private bool _leftTriggerPressed = false;
    private bool _rightTriggerPressed = false;

    void Start()
    {
        inputData = GetComponent<ControllerInputs>();
    }

    void Update()
    {
        // Check left controller trigger value
        if (inputData._leftController.isValid)
        {
            if (inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue))
            {
                if (leftTriggerValue > 0.1f && !_leftTriggerPressed)
                {
                    _leftTriggerPressed = true;
                    Debug.Log("Left Trigger Pressed");
                }
                else if (leftTriggerValue <= 0.1f && _leftTriggerPressed)
                {
                    _leftTriggerPressed = false;
                    Debug.Log("Left Trigger Released");
                }
            }
        }

        // Check right controller trigger value
        if (inputData._rightController.isValid)
        {
            if (inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue))
            {
                if (rightTriggerValue > 0.1f && !_rightTriggerPressed)
                {
                    _rightTriggerPressed = true;
                    Debug.Log("Right Trigger Pressed");
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
