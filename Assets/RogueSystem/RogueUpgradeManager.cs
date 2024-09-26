using System.Collections.Generic;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    public List<RogueUpgrade> Upgrades = new();

    public List<RogueUpgrade.RogueStat> Summary
    {
        get
        {
            // Create a blank Dictionary so we can assign stats w/o duplicating keys
            Dictionary<RogueUpgrade.RogueStatKey, int> summaryDict = new();

            // Start new list of upgrades to do some filtering
            List<RogueUpgrade> upgrades = new();

            foreach (var upgrade in Upgrades)
            {
                // If upgrade IS unique and there is not a copy added already
                // If upgrade is not unique
                if ((upgrade.Unique && !upgrades.Contains(upgrade)) || !upgrade.Unique)
                {
                    upgrades.Add(upgrade);
                }
            }

            // Go through each upgrade in filtered list of upgrades
            foreach (var upgrade in upgrades)
            {
                // Go through each stat
                foreach (var stat in upgrade.Stats)
                {
                    // If stat exists, increment by value, otherwise add the stat and value
                    if (summaryDict.ContainsKey(stat.Key))
                    {
                        summaryDict[stat.Key] += stat.Value;
                    }
                    else
                    {
                        summaryDict.Add(stat.Key, stat.Value);
                    }

                }
            }

            // Convert dict into list and ship
            List<RogueUpgrade.RogueStat> summary = new();

            foreach (var stat in summaryDict)
            {
                summary.Add(new RogueUpgrade.RogueStat(stat.Key, stat.Value));
            }

            return summary;
        }
    }

}
