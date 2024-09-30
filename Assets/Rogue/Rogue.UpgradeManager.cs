using System.Collections.Generic;
using UnityEngine;

namespace Rogue
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<UpgradeData> Upgrades = new();

        public List<Stat> Summary
        {
            get
            {
                // Create a blank Dictionary so we can assign stats w/o duplicating keys
                Dictionary<StatKey, Stat> summaryDict = new();

                // Start new list of upgrades to do some filtering
                List<UpgradeData> validUpgrades = new();

                foreach (var upgrade in Upgrades)
                {
                    // If upgrade IS unique and there is not a copy added already
                    // If upgrade is not unique
                    if ((upgrade.Unique && !validUpgrades.Contains(upgrade)) || !upgrade.Unique)
                    {
                        validUpgrades.Add(upgrade);
                    }
                }

                // Go through each upgrade in filtered list of upgrades
                foreach (var validUpgrade in validUpgrades)
                {
                    // Go through each stat
                    foreach (var stat in validUpgrade.Stats)
                    {
                        // If stat exists, increment by value, otherwise add the stat and value
                        if (summaryDict.ContainsKey(stat.Key))
                        {
                            summaryDict[stat.Key].IntValue += stat.IntValue;
                            summaryDict[stat.Key].FloatValue += stat.FloatValue;
                        }
                        else
                        {
                            summaryDict.Add(stat.Key, stat);
                        }

                    }
                }

                // Convert dict into list and ship
                List<Stat> summary = new();

                foreach (var stat in summaryDict)
                {
                    summary.Add(stat.Value);
                }

                return summary;
            }
        }

        public static UpgradeManager FindLive(GameObject gameObject)
        {
            if (GameObject.FindWithTag("LiveRogueUpgradeManager").TryGetComponent(out UpgradeManager upgradeManager))
            {
                return upgradeManager;
            }
            else
            {
                Debug.Log("Could not find Rogue Upgrade Manager", gameObject);
                return null;
            }
        }
    }

}
