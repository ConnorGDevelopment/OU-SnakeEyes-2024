using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Rogue
{
    public enum StatKey
    {
        None,
        Damage,
        Speed,
        FireRate,
        AmmoCapacity,
        ReloadSpeed,
        Health
    }

    [CreateAssetMenu(menuName = "RogueStatBlock")]
    public class StatBlock : ScriptableObject
    {
        [SerializedDictionary("StatKey", "Value")]
        public SerializedDictionary<Rogue.StatKey, float> Stats;

        public static SerializedDictionary<Rogue.StatKey, float> Sum(params StatBlock[] statBlocks)
        {
            SerializedDictionary<Rogue.StatKey, float> result = new();

            foreach (StatBlock statBlock in statBlocks)
            {
                foreach (var stat in statBlock.Stats)
                {
                    if (result.ContainsKey(stat.Key))
                    {
                        result[stat.Key] = result[stat.Key] + stat.Value;
                    }
                    else
                    {
                        result.Add(stat.Key, stat.Value);
                    }
                }
            }

            return result;
        }
        public static SerializedDictionary<Rogue.StatKey, float> Sum(params SerializedDictionary<Rogue.StatKey, float>[] statDicts)
        {
            SerializedDictionary<Rogue.StatKey, float> result = new();

            foreach (var statDict in statDicts)
            {
                foreach (var stat in statDict)
                {
                    if (result.ContainsKey(stat.Key))
                    {
                        result[stat.Key] = result[stat.Key] + stat.Value;
                    }
                    else
                    {
                        result.Add(stat.Key, stat.Value);
                    }
                }
            }

            return result;
        }

        public static int GetInt(SerializedDictionary<Rogue.StatKey, float> statDict, Rogue.StatKey statKey)
        {
            return statDict.ContainsKey(statKey) ? Mathf.RoundToInt(statDict[statKey]) : 0;
        }
    }
}
