using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rogue
{

    public class UpgradeManager : MonoBehaviour
    {
        public List<StatBlock> Upgrades = new();

        public StatDict Sum => Upgrades.Aggregate(new StatDict(), (prev, curr) => prev + curr.Stats);

        public static UpgradeManager FindManager()
        {
            if (GameObject.FindWithTag("RogueUpgradeManager").TryGetComponent(out UpgradeManager upgradeManager))
            {
                return upgradeManager;
            }
            else
            {
                Debug.Log("Could not find Rogue Upgrade Manager");
                return null;
            }
        }


    }

}