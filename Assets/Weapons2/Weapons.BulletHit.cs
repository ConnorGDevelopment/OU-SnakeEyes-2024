using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletHit : MonoBehaviour
    {
        public Rogue.UpgradeData RogueSnapshot;

        public float delayTime = 3f; // Optional delay to destroy the bullet after a certain time

        public void Init(Rogue.UpgradeData rogueSnapshot, Transform firepoint) {
            RogueSnapshot = rogueSnapshot;

            if(!RogueSnapshot.StatExists(Rogue.StatKey.Speed))
            {
                Debug.Log($"Bullet initialized without Speed value");
                return;
            }

            if (TryGetComponent(out Rigidbody rb)) {
                rb.velocity = firepoint.forward * rogueSnapshot.FindStat(Rogue.StatKey.Speed).FloatValue;
            } else
            {
                Debug.Log($"Bullet does not have RigidBody");
            }
        
        }

        // This method is called when the bullet enters a trigger collider
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object is tagged as "Enemy"
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Destroy the enemy and the bullet
                Destroy(other.gameObject); // Destroy the enemy the bullet hits
                Destroy(gameObject);       // Destroy the bullet
            }
        }
    }
}