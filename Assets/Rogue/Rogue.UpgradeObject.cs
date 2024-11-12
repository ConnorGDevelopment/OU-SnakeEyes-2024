using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace Rogue
{
    public class UpgradeObject : MonoBehaviour
    {
        public UpgradeData Upgrade;



        private UpgradeManager _upgradeManager;
        //private SpawningScript _spawningScript;
        public void Start()
        {
            _upgradeManager = UpgradeManager.FindLive(gameObject);
            //_spawningScript = SpawningScript.FindLive(gameObject);

        }

        public void OnGrab(SelectEnterEventArgs ctx)
        {
            Debug.Log("Click");
            _upgradeManager.Upgrades.Add(Upgrade);
            //_spawningScript.ResetEnemies();


        }
    }

}