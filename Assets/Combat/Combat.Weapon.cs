using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat
{
    public class Weapon : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public Transform Firepoint;

        public Rogue.UpgradeData BaseGunData;
        private string _interactorKey;


        private Rogue.UpgradeManager _upgradeManager;
        private Manager _combatManager;

        private int _currentBulletCount;
        public int CurrentBulletCount
        {
            get { return _currentBulletCount; }
            set
            {
                if (value > BaseGunData.FindStat(Rogue.StatKey.AmmoCapacity).IntValue)
                {
                    _currentBulletCount = BaseGunData.FindStat(Rogue.StatKey.AmmoCapacity).IntValue;
                }
                else if (value < 0)
                {
                    _currentBulletCount = 0;
                }
                else
                {
                    _currentBulletCount = value;
                }
            }
        }


        public Rogue.UpgradeData GunData
        {
            get
            {
                Rogue.UpgradeData upgradeData = ScriptableObject.CreateInstance<Rogue.UpgradeData>();
                upgradeData.CombineUpgrades(BaseGunData);
                upgradeData.CombineUpgrades(_upgradeManager.Summary);
                return upgradeData;
            }
        }

        public void Start()
        {
            if (BulletPrefab == null)
            {
                Debug.Log("No Bullet Prefab Assigned", gameObject);
            }

            if (BaseGunData == null)
            {
                Debug.Log("No GunData ScriptableObject Assigned", gameObject);
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

            _combatManager = Manager.FindLive(gameObject);
            _upgradeManager = Rogue.UpgradeManager.FindLive(gameObject);

        }

        public void Reload()
        {

            CurrentBulletCount = BaseGunData.FindStat(Rogue.StatKey.AmmoCapacity).IntValue;

        }

        // These functions are triggered by the Select Enter/Exit events on the XR Grab Interactable component
        // The Select Enter/Exit are triggered (only once) when grabbed or released
        public void OnGrab(SelectEnterEventArgs ctx)
        {
            // Set the WeaponMemory for this interactorObject, which is one of the hands, to the original state of this object
            _combatManager.RememberWeapon(ctx);

            _interactorKey = ctx.interactorObject.transform.gameObject.name;

            if (ctx.interactorObject.transform.gameObject.TryGetComponent(out ControllerEventHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.AddListener(FireBullet);
                Debug.Log($"Event Listener attached to {triggerHandler} by {gameObject}");
            }
            Debug.Log($"Revolver Grabbed by: {ctx.interactorObject}");

        }
        public void OnRelease(SelectExitEventArgs ctx)
        {
            if (ctx.interactorObject.transform.gameObject.TryGetComponent(out ControllerEventHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.RemoveAllListeners();
            }

            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = true;
            }

            Debug.Log($"Revolver Released by: {ctx.interactorObject}");

        }

        public void OnDestroy()
        {
            _combatManager.OnWeaponDestroyed.Invoke(_interactorKey);
        }

        // Function called when trigger pull
        public void FireBullet()
        {
            if (CurrentBulletCount > 0)
            {
                GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.transform.rotation);
                CurrentBulletCount--;
                Debug.Log($"Bullet Count Updated to {CurrentBulletCount} by {gameObject}");


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