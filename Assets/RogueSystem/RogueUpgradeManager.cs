using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    private List<RogueUpgrade> _upgrades = new();
    public List<RogueUpgrade> Upgrades {
        get { 
            List<RogueUpgrade> _filteredUpgrades = new();

            foreach (var upgrade in _upgrades) {
                if(upgrade.Unique == false)
                {
                    _filteredUpgrades.Add(upgrade);
                } else
                {
                    if(_filteredUpgrades.Select((toMatch) => toMatch.Name).Contains(upgrade.Name) == false) 
                    {
                        _filteredUpgrades.Add(upgrade);
                    }
                }
            }

            return _filteredUpgrades;
        }
    }

    public Dictionary<RogueStat,int> Summary
    {
        get
        {
            Dictionary<RogueStat, int> summary = new();


            foreach (var upgrade in Upgrades)
            {
                foreach (var stat in upgrade.Stats) {
                    if(summary.ContainsKey(stat.Key))
                    {
                        summary[stat.Key] += upgrade.Stats[stat.Key];
                    } else
                    {
                        summary.Add(stat.Key, stat.Value);
                    }
                }
            }

            return summary;
        }
    }

}
