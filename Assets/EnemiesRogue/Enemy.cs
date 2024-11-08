using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rogue.UpgradeData EnemyData;

    public void Start() {
        if (EnemyData == null) {
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
        set {
            if (value <= 0)
            {
                _currentHealth = 0;
                Destroy(gameObject);
            }
            else { 
                _currentHealth = value; 
            }
        }
    }
}