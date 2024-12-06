using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private bool isSceneLoading = false; // Prevent multiple scene loads

    private void OnTriggerEnter(Collider other)
    {
        if (isSceneLoading) return; // Exit if a scene is already loading

        if (other.CompareTag("Enemy"))
        {
            isSceneLoading = true;
            SceneManager.LoadScene("Contract Room");
        }
        else if (other.CompareTag("Contract"))
        {
            isSceneLoading = true;
            SceneManager.LoadScene("RogueSystemTest");
        }
    }
}
