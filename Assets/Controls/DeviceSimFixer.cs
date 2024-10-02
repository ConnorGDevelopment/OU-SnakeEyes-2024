using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSimFixer : MonoBehaviour
{
    public bool IsDirty = false;

    public void Update()
    {
        if (!IsDirty)
        {
            foreach (var item in Input.GetJoystickNames())
            {
                Debug.Log($"{item}");
            }
        }
        IsDirty = true;
    }

}
