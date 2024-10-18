using System.Collections.Generic;
using UnityEngine;

namespace Rogue
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<UpgradeData> Upgrades = new();

        public UpgradeData Summary
        {
            get
            {
                List<UpgradeData> validUpgrades = new();

                Upgrades.ForEach(upgrade => { 
                    if(!upgrade.Unique || (upgrade.Unique && !validUpgrades.Exists(match => match.Name == upgrade.Name)))
                    {
                        validUpgrades.Add(upgrade);
                    } 
                });

                UpgradeData summary = new();

                // Go through each upgrade in filtered list of upgrades
                foreach (var validUpgrade in validUpgrades)
                {
                    summary.CombineUpgrades(validUpgrade);
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
