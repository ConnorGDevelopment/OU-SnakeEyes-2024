using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    public readonly List<RogueUpgrade> Upgrades = new List<RogueUpgrade>();

    public RogueUpgrade Summary
    {
        get {
            RogueUpgrade _summary = new();

            List<RogueUpgrade> _upgrades = new List<RogueUpgrade>();

            foreach (var upgrade in Upgrades) {
                if (upgrade.Unique)
                {
                    if(_upgrades.Find(match => match.Name == upgrade.Name)) { 
                        // Don't add if match => need to flip if statement
                    }
                }
            };

            foreach (var upgrade in Upgrades)
            {
                _summary.Damage += upgrade.Damage;
                _summary.Speed += upgrade.Speed;
                _summary.Projectiles += upgrade.Projectiles;
                _summary.Spread
                            += upgrade.Spread;
            }

            return _summary;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
