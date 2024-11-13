using Rogue;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat
{
    public interface IWeaponMemory
    {
        public Material Material { get; }
        public Mesh Mesh { get; }
        public UpgradeData UpgradeData { get; }
    }

    public class Weapon : MonoBehaviour
    {
        // This should be a copy of the gun's prefab, prefab data disappear at runtime
        private GameObject _original;

        

        public GameObject BulletPrefab;
        public UpgradeData BaseGunData;
        public Transform Firepoint;
        private UpgradeManager _upgradeManager;
        private Combat.Manager _combatManager;

        private int _currentBulletCount;
        public int CurrentBulletCount
        {
            get { return _currentBulletCount; }
            set
            {
                if (value > BaseGunData.FindStat(StatKey.AmmoCapacity).IntValue)
                {
                    _currentBulletCount = BaseGunData.FindStat(StatKey.AmmoCapacity).IntValue;
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


        public UpgradeData GunData
        {
            get
            {
                UpgradeData upgradeData = ScriptableObject.CreateInstance<UpgradeData>();
                upgradeData.CombineUpgrades(BaseGunData);
                upgradeData.CombineUpgrades(_upgradeManager.Summary);
                return upgradeData;
            }
        }

        public void Start()
        {
            // Saving gameObject, which should be the prefab
            _original = gameObject;

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


            _upgradeManager = UpgradeManager.FindLive(gameObject);
        }

        // These functions are triggered by the Select Enter/Exit events on the XR Grab Interactable component
        // The Select Enter/Exit are triggered (only once) when grabbed or released
        public void OnGrab(SelectEnterEventArgs ctx)
        {
            // Set the WeaponMemory for this interactorObject, which is one of the hands, to the original state of this object
            _upgradeManager.WeaponMemory[ctx.interactorObject.ToString()] = _original;
            Debug.Log($"{BaseGunData.Name} remembered for {ctx.interactorObject}");
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