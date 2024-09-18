using System.Collections.Generic;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    // Readonly list means elements can be added/removed but the list can't be replaced
    public readonly List<RogueUpgrade> Upgrades = new();

    public RogueUpgrade Summary
    {
        get
        {
            // Create a blank RogueUpgrade so we can assign stats
            RogueUpgrade summary = ScriptableObject.CreateInstance<RogueUpgrade>();

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
                    if (summary.Stats.ContainsKey(stat.Key))
                    {
                        summary.Stats[stat.Key] += stat.Value;
                    }
                    else
                    {
                        summary.Stats.Add(stat.Key, stat.Value);
                    }
                }
            }

            return summary;
        }
    }

}
