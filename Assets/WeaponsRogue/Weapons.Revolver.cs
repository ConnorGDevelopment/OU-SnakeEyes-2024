using Rogue;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class Revolver : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public UpgradeData GunData;
        public Transform Firepoint;
        private UpgradeManager _upgradeManager;
        // Common Backing Field usage, basically set guards to keep the AmmoCount within a certain range: 0 and the Max 
        private int _currentAmmoCount;
        public int CurrentAmmoCount { 
            get { return _currentAmmoCount; }
            set
            {
                // Don't set value to larger than AmmoCapacity
                if (value > GunData.FindStat(StatKey.AmmoCapacity).IntValue)
                {
                    _currentAmmoCount = GunData.FindStat(StatKey.AmmoCapacity).IntValue;
                }
                // Don't set value to negative
                else if (value < 0)
                {
                    _currentAmmoCount = 0;
                }
                else { 
                    _currentAmmoCount = value;    
                }
            }
        }

        public void Start()
        {
            if (BulletPrefab == null)
            {
                Debug.Log("No Bullet Prefab Assigned", gameObject);
            }

            if (GunData == null)
            {
                Debug.Log("No default values, GunData not assigned an UpgradeData SO", gameObject);

            }
            else {
                if (GunData.StatExists(StatKey.AmmoCapacity))
                {
                    _currentAmmoCount = GunData.FindStat(StatKey.AmmoCapacity).IntValue;
                }
                else {
                    Debug.Log($"No AmmoCapacity stat in GunData: {GunData.Stats}", gameObject);
                }
            }

            // Grab Bullet Spawn from child
            if (gameObject.GetNamedChild("Firepoint").TryGetComponent(out Transform firepoint))
            {
                Firepoint = firepoint;
            }
            else
            {
                Debug.Log("No Child named Firepoint", gameObject);
            }


            _upgradeManager = UpgradeManager.FindLive(gameObject);
        }

        // These functions are triggered by the Select Enter/Exit events on the XR Grab Interactable component
        // The Select Enter/Exit are triggered (only once) when grabbed or released
        public void OnGrab(SelectEnterEventArgs ctx)
        {
            if (ctx.interactorObject.transform.gameObject.TryGetComponent(out TriggerHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.AddListener(FireBullet);
                Debug.Log($"Event Listener attached to {triggerHandler} by {gameObject}");
            }
            Debug.Log($"Revolver Grabbed by: {ctx.interactorObject}");

        }
        public void OnRelease(SelectExitEventArgs ctx)
        {
            if (ctx.interactorObject.transform.gameObject.TryGetComponent(out TriggerHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.RemoveAllListeners();
            }
            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = true;
            }
            Debug.Log($"Revolver Released by: {ctx.interactorObject}");

        }

        // Function called when trigger pull
        public void FireBullet()
        {
            if (CurrentAmmoCount > 0)
            {
                GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.transform.rotation);
                CurrentAmmoCount--; // This runs through the setter we wrote above
                Debug.Log($"Bullet Count Updated to {CurrentAmmoCount} by {gameObject}");


                if (bullet.TryGetComponent(out BulletHit bulletHit))
                {
                    bulletHit.Init(GunData, Firepoint);
                    Debug.Log($"{gameObject.name} Fired Bullet", gameObject);
                }
                else
                {
                    Debug.Log($"Bullet does not have BulletHit component");
                }
            }

        }
    }
}
