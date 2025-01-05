using Rogue;
using UnityEngine;

namespace Enemies
{

    public class Enemy : MonoBehaviour
    {
        public StatBlock EnemyData;
        public AudioSource ProximityWarningSource;

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
                Debug.Log("No default values, EnemyData not assigned an UpgradeData SO", gameObject);
            }

            if (ProximityWarningSource == null)
            {
                Debug.Log("No Audio Source set for Proximity Warning", gameObject);
            }
            else if (ProximityWarningSource.clip == null)
            {
                Debug.Log("No Audio Clip set in Audio Source for Proximity Warning", gameObject);
            }
        }

        public void Update()
        {
            if (CurrentHealth == 0)
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ProximityWarning"))
            {
                ProximityWarningSource.PlayOneShot(ProximityWarningSource.clip, 1f);
            }
        }
    }
}