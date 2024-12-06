using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat
{

    public class Weapon : MonoBehaviour
    {
        // GameObject References
        private Rogue.UpgradeManager _upgradeManager;
        private Transform _bulletSpawn;
        private Combat.ControllerEventHandler _controllerEventHandler;
        public GameObject BulletPrefab;


        // Rogue Stats
        public Rogue.StatBlock BaseStats;

        public Rogue.StatDict CurrentStats
        {
            get
            {
                return BaseStats.Stats + _upgradeManager.Sum;
            }
        }

        // Ammo
        private int _currentAmmoCount;
        public int CurrentAmmoCount
        {
            get { return _currentAmmoCount; }
            set => _currentAmmoCount = value < 0
                    ? 0
                    : value > CurrentStats.GetInt(Rogue.StatKey.AmmoCapacity)
                        ? CurrentStats.GetInt(Rogue.StatKey.AmmoCapacity)
                        : value;
        }
        public void ReloadAmmo()
        {
            CurrentAmmoCount = CurrentStats.GetInt(Rogue.StatKey.AmmoCapacity);
        }

        public void Start()
        {
            if (BulletPrefab == null)
            {
                Debug.Log("No Bullet Prefab Assigned", gameObject);
            }

            if (BaseStats == null)
            {
                Debug.Log("No BaseStats Assigned", gameObject);
            }

            // Grab Bullet Spawn from child
            if (gameObject.GetNamedChild("BulletSpawn").TryGetComponent(out Transform bulletSpawn))
            {
                _bulletSpawn = bulletSpawn;
            }
            else
            {
                Debug.Log("No Child named BulletSpawn", gameObject);
            }
            _upgradeManager = Rogue.UpgradeManager.FindManager();
            CurrentAmmoCount = CurrentStats.GetInt(Rogue.StatKey.AmmoCapacity);

        }



        // These functions are triggered by the Select Enter/Exit events on the XR Grab Interactable component
        // The Select Enter/Exit are triggered (only once) when grabbed or released
        public void OnGrab(SelectEnterEventArgs ctx)
        {

            if (ctx.interactorObject.transform.parent.TryGetComponent(out ControllerEventHandler controllerEventHandler))
            {
                _controllerEventHandler = controllerEventHandler;
                _controllerEventHandler.ImprintWeapon(gameObject);
                _controllerEventHandler.OnActivate.AddListener(FireBullet);
            }


        }
        public void OnRelease(SelectExitEventArgs ctx)
        {
            _controllerEventHandler.OnActivate.RemoveAllListeners();
        }

        public void OnDestroy()
        {
            if (_controllerEventHandler)
            {
                _controllerEventHandler.WeaponDestroyed.Invoke();
            }
        }

        // Function called when trigger pull
        public void FireBullet()
        {
            if (CurrentAmmoCount > 0)
            {
                GameObject bullet = Instantiate(BulletPrefab, _bulletSpawn.position, _bulletSpawn.transform.rotation);
                CurrentAmmoCount--;
                bullet.GetComponent<Combat.Bullet>().Init(_upgradeManager.Sum, _bulletSpawn);
                Debug.Log($"{gameObject.name} Fired Bullet", gameObject);
            }

        }
    }
}