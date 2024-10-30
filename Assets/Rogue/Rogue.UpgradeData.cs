using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Rogue
{
  

    [CreateAssetMenu(menuName = "RogueUpgrade")]
    public class UpgradeData : ScriptableObject
    {
        public string Name;
        // If Unique == true, duplicates of this upgrade don't stack
        public bool Unique;

        public List<Stat> Stats = new();

        public Stat FindStat(StatKey key)
        {
            return Stats.Find(match => match.Key == key);
        }
        public Stat FindStat(Stat stat) { 
            return Stats.Find(match => match.Key == stat.Key);
        }

        public bool StatExists(StatKey key)
        {
            return Stats.Exists(match => match.Key == key);
        }
        public bool StatExists(Stat stat) { 
            return Stats.Exists(match => match.Key == stat.Key);
        }

        public void CombineUpgrades(UpgradeData upgrade) {
            foreach (var stat in upgrade.Stats.Where(stat => StatExists(stat)))
            {
                if (FindStat(stat) != null)
                {
                    FindStat(stat).CombineStat(stat);
                }
                else { 
                    Stats.Add(stat);
                }
            }
        }
    }
}
