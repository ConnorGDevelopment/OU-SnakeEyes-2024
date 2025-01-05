using AYellowpaper.SerializedCollections;
using Enemies;
using Rogue;
using UnityEngine;

namespace Combat
{

    public class Bullet : MonoBehaviour
    {
        public SerializedDictionary<StatKey, float> RogueSnapshot;

        public void Init(SerializedDictionary<StatKey, float> rogueSnapshot, Transform firepoint)
        {
            RogueSnapshot = rogueSnapshot;
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = firepoint.forward * RogueSnapshot[StatKey.Speed];
            }
            else
            {
                Debug.Log("Bullet does not have RigidBody");
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.CurrentHealth -= StatBlock.GetInt(RogueSnapshot, StatKey.Damage);
                Destroy(gameObject);
            }

            if (collision.gameObject.TryGetComponent(out TerrainCollider _))
            {
                Destroy(gameObject);
            }
        }
    }

}