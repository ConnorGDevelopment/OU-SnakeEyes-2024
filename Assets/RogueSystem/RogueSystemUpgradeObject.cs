using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RogueSystemUpgradeObject : MonoBehaviour
{
    public RogueUpgrade Upgrade;

    private RogueUpgradeManager _rogueUpgradeManager;

    public void Start()
    {
        if (GameObject.FindWithTag("LiveRogueUpgradeManager").TryGetComponent(out RogueUpgradeManager liveRogueUpgradeManager))
        {
            _rogueUpgradeManager = liveRogueUpgradeManager;
        }
        else
        {
            Debug.Log("Could not find Rogue Upgrade Manager", gameObject);
        }
    }

    public void OnGrab(SelectEnterEventArgs ctx)
    {
        Debug.Log("Click");
        _rogueUpgradeManager.Upgrades.Add(Upgrade);
    }
}
