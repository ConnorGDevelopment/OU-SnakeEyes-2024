using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject UpgradeCube;
    private Vector3 spawn = new Vector3(-0.75f, 1.3f, 3.5f);
    private bool UpgradesSpawned = false;

    public SpawningScript waveFinished;

    // Start is called before the first frame update
    void Start()
    {
        waveFinished = FindObjectOfType<SpawningScript>();

        if(waveFinished != null )
        {
            Debug.Log("why are you missing?");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (waveFinished.waveEnded == true && !UpgradesSpawned)
        {

            Vector3 currentspawn = spawn;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(UpgradeCube, currentspawn, Quaternion.identity);
                currentspawn.x += 0.75f;
            }

            
            UpgradesSpawned = true;
        }
    }








}
