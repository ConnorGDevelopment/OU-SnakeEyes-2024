using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rogue
{
    public enum StatKey
    {
        None,
        Damage,
        Speed,
        FireRate,
        AmmoCapacity
    }

    public class StatDict : Dictionary<Rogue.StatKey, float>
    {
        public StatDict(Dictionary<Rogue.StatKey, float> statBlock = null)
        {
            foreach (var statKey in Enum.GetNames(typeof(Rogue.StatKey)))
            {
                this[Enum.Parse<Rogue.StatKey>(statKey)] = 0;
            }

            if (statBlock != null)
            {
                foreach (var stat in statBlock)
                {
                    this[stat.Key] += stat.Value;
                }
            }
        }

        // 'operator': This modifier basically says "Don't use this as a function, I mean the literal operator"
        // 'static': This function can be called from this class no matter what, even if you don't have an instance of a class
        public static StatDict operator +(StatDict statBlockA, StatDict statBlockB)
        {
            StatDict result = new StatDict(statBlockA);

            foreach (var stat in statBlockB)
            {
                // Ternary Operator: (condition) ? (value if true) : (value if false)
                // You can also chain these further and put them on multiple lines
                // Usually its easier to read if you chain off false condition, like this:
                // (condition1)
                //      ? (value if condition1 == true)
                //      : (condition2)
                //          ? (value if condition2 == true)
                //          : (value if condition2 == false
                statBlockA[stat.Key] = statBlockA.ContainsKey(stat.Key) ? (statBlockA[stat.Key] + stat.Value) : stat.Value;

            }

            return result;
        }

        public static StatDict operator -(StatDict statBlockA, StatDict statBlockB)
        {
            StatDict result = new StatDict(statBlockA);

            // Linq loop suggested by SonarLint
            // Normal foreach loop uses 'type x in y'
            // 'from' works kind of like 'out' here, it pulls the value out of an enumerable (list, array, dict, etc)
            // 'where (condition)' makes it so only the items that pass the condition are returned
            // 'select' just lets you specify what you are returning as the value, here it is just the whole thing but it could be just 'stat.Key' or 'stat.Value'
            foreach (var stat in from stat in statBlockB
                                 where statBlockA.ContainsKey(stat.Key)
                                 select stat)
            {
                statBlockA[stat.Key] -= stat.Value;
            }

            return result;
        }

        public int GetInt(Rogue.StatKey statKey)
        {
            return ContainsKey(statKey) ? Mathf.RoundToInt(this[statKey]) : 0;
        }
    }
}