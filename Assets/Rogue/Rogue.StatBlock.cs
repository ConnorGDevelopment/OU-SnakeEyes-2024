using System.Collections.Generic;

namespace Rogue {
	public class StatBlock {
        public Dictionary<Rogue.StatKey, float> Stats = new();

        public StatBlock(Dictionary<Rogue.StatKey, float> stats = null) {
            if (stats != null) { 
                Stats = stats;  
            }   
        }

        public static StatBlock operator +(StatBlock statBlockA, StatBlock statBlockB) { 
            StatBlock result = new StatBlock(statBlockA.Stats);

            foreach (var stat in statBlockB.Stats)
            {
                if (statBlockA.Stats.ContainsKey(stat.Key))
                {
                    statBlockA.Stats[stat.Key] += stat.Value;
                }
                else { 
                    statBlockA.Stats.Add(stat.Key, stat.Value);
                }
            }

            return result;
        }

        public static StatBlock operator -(StatBlock statBlockA, StatBlock statBlockB)
        {
            StatBlock result = new StatBlock(statBlockA.Stats);

            foreach (var stat in statBlockB.Stats)
            {
                if (statBlockA.Stats.ContainsKey(stat.Key))
                {
                    statBlockA.Stats[stat.Key] -= stat.Value;
                }
                else
                {
                    statBlockA.Stats.Remove(stat.Key);
                }
            }

            return result;
        }

        public static bool operator ==(StatBlock statBlockA, StatBlock statBlockB)
        {
            bool result = true;

            

            foreach (var stat in statBlockB.Stats) {
                if (statBlockA.Stats.ContainsKey(stat.Key))
                {
                    if (statBlockA.Stats[stat.Key] != statBlockB.Stats[stat.Key]) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            return result;

        }
    }
}