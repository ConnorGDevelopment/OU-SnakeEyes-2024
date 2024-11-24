using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons;

[System.Serializable]
public class SoundManagement : MonoBehaviour
{


    public AudioClip RevolverSound;
    private Revolver _pewpew;
    private AudioSource _source;
    public Revolver RevolverReference;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _pewpew = GetComponent<Revolver>();
    }




    public static SoundManagement ClassFind(GameObject gameObject)
    {
        if(GameObject.FindWithTag("SoundManager").TryGetComponent(out SoundManagement soundmanagement))
        {
            return soundmanagement;
        }

        else 
        {
            Debug.Log("oh poop");
            return null;
        }

    }



}
