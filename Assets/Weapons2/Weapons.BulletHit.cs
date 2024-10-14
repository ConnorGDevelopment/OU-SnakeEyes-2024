using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletHit : MonoBehaviour
    {
        public Rogue.UpgradeData BulletData;
        public float velocity = 20f; // Bullet velocity
        public float delayTime = 3f; // Optional delay to destroy the bullet after a certain time

        
        // Start is called before the first frame update
        void Start()
        {
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {

                // Apply velocity in the forward direction of the bullet
                rb.velocity = transform.forward * velocity;

                // Destroy the bullet after a delay to avoid it lingering indefinitely
                Destroy(gameObject, delayTime);
            }
            else
            {
                Debug.Log("Screeeeeeeeeech");
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