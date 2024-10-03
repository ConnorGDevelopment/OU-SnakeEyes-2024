using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestruction : MonoBehaviour
{

    public delegate void EnemyDestroyedHandler(GameObject enemy);
    public event EnemyDestroyedHandler OnDestroyed;

    private void OnDestroy()
    {
        if (OnDestroyed != null)  //Letting the SpawningScript know that this enemy was destroyed
        {
            OnDestroyed(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
