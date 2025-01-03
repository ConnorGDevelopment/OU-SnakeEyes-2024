using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Combat
{

    public class ControllerEventHandler : MonoBehaviour
    {
        public Transform HolsterTransform;

        public GameObject WeaponImprint;

        public UnityEvent WeaponDestroyed = new();

        public UnityEvent OnActivate = new();

        public void ImprintWeapon(GameObject gameObject)
        {
            if (WeaponImprint != null && WeaponImprint.GetComponent<Weapon>().BaseStats != gameObject.GetComponent<Weapon>().BaseStats)
            {
                Object.Destroy(WeaponImprint);
                WeaponImprint = null;
            }
            if (WeaponImprint == null)
            {
                WeaponImprint = Object.Instantiate(gameObject);
                WeaponImprint.transform.SetParent(HolsterTransform, worldPositionStays: false);
                WeaponImprint.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                WeaponImprint.SetActive(value: false);
            }
        }

        public void SpawnWeapon()
        {
            GameObject obj = Object.Instantiate(WeaponImprint, HolsterTransform);
            obj.SetActive(value: true);
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }

        public void Start()
        {
            if (!HolsterTransform)
            {
                Debug.Log($"No Holster Transform set for {base.gameObject}", base.gameObject);
            }
            WeaponDestroyed.AddListener(SpawnWeapon);
        }

        public void HandleActivate(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValueAsButton())
            {
                Debug.Log("HandleActivate on " + base.gameObject.name + " called on " + ctx.action.name);
                OnActivate.Invoke();
            }
        }
    }
}