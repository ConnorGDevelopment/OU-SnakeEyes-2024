using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Rogue;

public class UpgradeManager : MonoBehaviour
{
	public List<StatBlock> Upgrades = new List<StatBlock>();

	public SerializedDictionary<StatKey, float> Current => StatBlock.Sum(Upgrades.ToArray());

	public static UpgradeManager FindManager()
	{
		if (GameObject.FindWithTag("RogueUpgradeManager").TryGetComponent<UpgradeManager>(out var component))
		{
			return component;
		}
		Debug.Log("Could not find Rogue Upgrade Manager");
		return null;
	}
}
