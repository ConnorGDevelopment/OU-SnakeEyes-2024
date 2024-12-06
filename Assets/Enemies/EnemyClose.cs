using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClose : MonoBehaviour
{

    public AudioSource _audiosource;
    public AudioClip _ghostSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyClose"))
        {
            _audiosource.PlayOneShot(_ghostSound, 1f);
        }
        else
        {
            Debug.Log("hello why arent we playing the sound?");
        }
    }
}
