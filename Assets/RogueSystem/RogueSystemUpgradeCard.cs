using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSystemUpgradeCard : MonoBehaviour
{
    public RogueUpgrade Upgrade;    

    private RogueUpgradeManager _rogueUpgradeManager;

    public void Start() {
        if (GameObject.FindWithTag("LiveRogueUpgradeManager").TryGetComponent(out RogueUpgradeManager liveRogueUpgradeManager))
        {
            _rogueUpgradeManager = liveRogueUpgradeManager;
        }
        else {
            Debug.Log("Could not find Rogue Upgrade Manager", gameObject);
        }
    }

    public void OnSelect() {
        Debug.Log("Click");
       _rogueUpgradeManager.Upgrades.Add(Upgrade);
    }
}
