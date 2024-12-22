using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Rogue;

public class UpgradeObject : MonoBehaviour
{
	public StatBlock Upgrade;

	private UpgradeManager _upgradeManager;

	public void Start()
	{
		_upgradeManager = UpgradeManager.FindManager();
	}

	public void OnGrab(SelectEnterEventArgs ctx)
	{
		Debug.Log("Click");
		_upgradeManager.Upgrades.Add(Upgrade);
		Object.Destroy(base.gameObject);
	}
}
