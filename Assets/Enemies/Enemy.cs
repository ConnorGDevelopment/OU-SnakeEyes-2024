using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public Rogue.StatBlock EnemyData;

        public void Start()
        {
            if (EnemyData == null)
            {
                Debug.Log("No default values, EnemyData not assigned an UpgradeData SO", gameObject);
            }
        }

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

        public void Update()
        {
            if (CurrentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}