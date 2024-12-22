using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Rogue;

[CreateAssetMenu(menuName = "RogueStatBlock")]
public class StatBlock : ScriptableObject
{
	public SerializedDictionary<StatKey, float> Stats;

	public static SerializedDictionary<StatKey, float> Sum(params StatBlock[] statBlocks)
	{
		SerializedDictionary<StatKey, float> serializedDictionary = new SerializedDictionary<StatKey, float>();
		for (int i = 0; i < statBlocks.Length; i++)
		{
			foreach (KeyValuePair<StatKey, float> stat in statBlocks[i].Stats)
			{
				if (serializedDictionary.ContainsKey(stat.Key))
				{
					serializedDictionary[stat.Key] = serializedDictionary[stat.Key] + stat.Value;
				}
				else
				{
					serializedDictionary.Add(stat.Key, stat.Value);
				}
			}
		}
		return serializedDictionary;
	}

	public static SerializedDictionary<StatKey, float> Sum(params SerializedDictionary<StatKey, float>[] statDicts)
	{
		SerializedDictionary<StatKey, float> serializedDictionary = new SerializedDictionary<StatKey, float>();
		for (int i = 0; i < statDicts.Length; i++)
		{
			foreach (KeyValuePair<StatKey, float> item in statDicts[i])
			{
				if (serializedDictionary.ContainsKey(item.Key))
				{
					serializedDictionary[item.Key] = serializedDictionary[item.Key] + item.Value;
				}
				else
				{
					serializedDictionary.Add(item.Key, item.Value);
				}
			}
		}
		return serializedDictionary;
	}

	public static int GetInt(SerializedDictionary<StatKey, float> statDict, StatKey statKey)
	{
		if (!statDict.ContainsKey(statKey))
		{
			return 0;
		}
		return Mathf.RoundToInt(statDict[statKey]);
	}
}
