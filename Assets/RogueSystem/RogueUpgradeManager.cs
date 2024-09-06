using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    public readonly List<RogueUpgrade> Upgrades = new();

    public RogueUpgrade Summary
    {
        get {
            

            foreach (var upgrade in Upgrades.Distinct())
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
