using Rogue;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class Revolver : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public GunData GunData;
        public Transform Firepoint;
        private Rogue.UpgradeManager _upgradeManager;

        public void Start()
        {
            if (BulletPrefab == null)
            {
                Debug.Log("No Bullet Prefab Assigned", gameObject);
            }

            if (GunData == null)
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
            if (ctx.interactorObject.transform.gameObject.TryGetComponent<TriggerHandler>(out TriggerHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.AddListener(FireBullet);
            }
            Debug.Log($"Revolver Grabbed by: {ctx.interactorObject}");

        }
        public void OnRelease(SelectExitEventArgs ctx)
        {
            if (ctx.interactorObject.transform.gameObject.TryGetComponent<TriggerHandler>(out TriggerHandler triggerHandler))
            {
                triggerHandler.OnTriggerPull.RemoveAllListeners();
            }
            Debug.Log($"Revolver Released by: {ctx.interactorObject}");

        }

        // Function called when trigger pull
        private void FireBullet()
        {
            Debug.Log($"{gameObject.name} Fired Bullet", gameObject);
            GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, BulletPrefab.transform.rotation);

            if (bullet.TryGetComponent(out Rigidbody bulletRb))
            {
                bulletRb.velocity = Firepoint.forward * GunData.Stats.Find(stat => stat.Key == StatKey.Speed).FloatValue;
            }
        }
    }
}
