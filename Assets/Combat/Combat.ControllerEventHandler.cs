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
            if (WeaponImprint != null && (WeaponImprint.GetComponent<Combat.Weapon>().BaseStats.Name != gameObject.GetComponent<Combat.Weapon>().BaseStats.Name))
            {
                Destroy(WeaponImprint);
                WeaponImprint = null;
            }

            if (WeaponImprint == null)
            {
                WeaponImprint = Instantiate(gameObject);
                WeaponImprint.transform.SetParent(HolsterTransform, false);
                WeaponImprint.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                WeaponImprint.SetActive(false);
            }
        }
        public void SpawnWeapon()
        {
            Instantiate(WeaponImprint);
        }

        public void Start()
        {
            if (!HolsterTransform)
            {
                Debug.Log($"No Holster Transform set for {gameObject}", gameObject);
            }
        }

        public void HandleActivate(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValueAsButton())
            {
                Debug.Log($"HandleActivate on {gameObject.name} called on {ctx.action.name}");
                OnActivate.Invoke();
            }
        }
    }
}