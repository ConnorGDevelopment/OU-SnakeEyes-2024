using AYellowpaper.SerializedCollections;
using Rogue;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat;

public class Weapon : MonoBehaviour
{
	private UpgradeManager _upgradeManager;

	private Transform _bulletSpawn;

	private ControllerEventHandler _controllerEventHandler;

	public GameObject BulletPrefab;

	public StatBlock BaseStats;

	private int _currentAmmoCount;

	public SerializedDictionary<StatKey, float> CurrentStats => StatBlock.Sum(BaseStats.Stats, _upgradeManager.Current);

	public int CurrentAmmoCount
	{
		get
		{
			return _currentAmmoCount;
		}
		set
		{
			_currentAmmoCount = ((value >= 0) ? ((value > StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity)) ? StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity) : value) : 0);
		}
	}

	public void ReloadAmmo()
	{
		CurrentAmmoCount = StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity);
	}

	public void Start()
	{
		if (BulletPrefab == null)
		{
			Debug.Log("No Bullet Prefab Assigned", base.gameObject);
		}
		if (BaseStats == null)
		{
			Debug.Log("No BaseStats Assigned", base.gameObject);
		}
		if (base.gameObject.GetNamedChild("BulletSpawn").TryGetComponent<Transform>(out var component))
		{
			_bulletSpawn = component;
		}
		else
		{
			Debug.Log("No Child named BulletSpawn", base.gameObject);
		}
		_upgradeManager = UpgradeManager.FindManager();
		CurrentAmmoCount = StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity);
	}

	public void OnGrab(SelectEnterEventArgs ctx)
	{
		if (ctx.interactorObject.transform.parent.TryGetComponent<ControllerEventHandler>(out var component))
		{
			_controllerEventHandler = component;
			_controllerEventHandler.ImprintWeapon(base.gameObject);
			_controllerEventHandler.OnActivate.AddListener(FireBullet);
		}
	}

	public void OnRelease(SelectExitEventArgs ctx)
	{
		_controllerEventHandler.OnActivate.RemoveAllListeners();
	}

	public void OnDestroy()
	{
		if ((bool)_controllerEventHandler)
		{
			_controllerEventHandler.WeaponDestroyed.Invoke();
		}
	}

	public void FireBullet()
	{
		Debug.Log(CurrentStats[StatKey.AmmoCapacity]);
		if (CurrentAmmoCount > 0)
		{
			GameObject obj = Object.Instantiate(BulletPrefab, _bulletSpawn.position, _bulletSpawn.transform.rotation);
			CurrentAmmoCount--;
			obj.GetComponent<Bullet>().Init(CurrentStats, _bulletSpawn);
			Debug.Log(base.gameObject.name + " Fired Bullet", base.gameObject);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.TryGetComponent<TerrainCollider>(out var _))
		{
			Object.Destroy(base.gameObject, CurrentStats[StatKey.ReloadSpeed]);
		}
	}
}