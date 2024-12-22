using AYellowpaper.SerializedCollections;
using Enemies;
using Rogue;
using UnityEngine;

namespace Combat;

public class Bullet : MonoBehaviour
{
	public SerializedDictionary<StatKey, float> RogueSnapshot;

	public float delayTime = 3f;

	public void Init(SerializedDictionary<StatKey, float> rogueSnapshot, Transform firepoint)
	{
		RogueSnapshot = rogueSnapshot;
		if (TryGetComponent<Rigidbody>(out var component))
		{
			component.velocity = firepoint.forward * RogueSnapshot[StatKey.Speed];
		}
		else
		{
			Debug.Log("Bullet does not have RigidBody");
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent<Enemy>(out var component))
		{
			component.CurrentHealth -= StatBlock.GetInt(RogueSnapshot, StatKey.Damage);
			Object.Destroy(base.gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<TerrainCollider>(out var _))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
