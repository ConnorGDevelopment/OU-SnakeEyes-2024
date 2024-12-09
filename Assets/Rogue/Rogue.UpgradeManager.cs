using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace Rogue
{

    public class UpgradeManager : MonoBehaviour
    {
        public List<StatBlock> Upgrades = new();

        public SerializedDictionary<Rogue.StatKey, float> Current => StatBlock.Sum(Upgrades.ToArray());


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