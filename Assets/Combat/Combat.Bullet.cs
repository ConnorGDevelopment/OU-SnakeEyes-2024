using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        public Rogue.StatDict RogueSnapshot;

        public float delayTime = 3f; // Optional delay to destroy the bullet after a certain time

        public void Init(Rogue.StatDict rogueSnapshot, Transform firepoint)
        {
            RogueSnapshot = rogueSnapshot;

            if (TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = firepoint.forward * RogueSnapshot[Rogue.StatKey.Speed];
            }
            else
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