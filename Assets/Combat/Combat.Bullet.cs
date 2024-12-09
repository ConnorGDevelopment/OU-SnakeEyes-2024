using AYellowpaper.SerializedCollections;
using Enemies;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        public SerializedDictionary<Rogue.StatKey, float> RogueSnapshot;

        public float delayTime = 3f; // Optional delay to destroy the bullet after a certain time

        public void Init(SerializedDictionary<Rogue.StatKey, float> rogueSnapshot, Transform firepoint)
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
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                // Destroy the enemy and the bullet
                enemy.CurrentHealth -= Rogue.StatBlock.GetInt(RogueSnapshot, Rogue.StatKey.Damage);
                Destroy(gameObject);       // Destroy the bullet
            }
        }
    }
}