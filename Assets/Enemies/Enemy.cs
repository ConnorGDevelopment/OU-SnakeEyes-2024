using Rogue;
using UnityEngine;

namespace Enemies;

public class Enemy : MonoBehaviour
{
	public StatBlock EnemyData;

	public int _currentHealth;

	public int CurrentHealth
	{
		get
		{
			return _currentHealth;
		}
		set
		{
			if (value <= 0)
			{
				_currentHealth = 0;
			}
			else
			{
				_currentHealth = value;
			}
		}
	}

	public void Start()
	{
		if (EnemyData == null)
		{
			Debug.Log("No default values, EnemyData not assigned an UpgradeData SO", base.gameObject);
		}
	}

	public void Update()
	{
		if (CurrentHealth == 0)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
