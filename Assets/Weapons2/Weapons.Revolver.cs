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
        public Rogue.UpgradeData GunData;
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

            if (gameObject.TryGetComponent(out Rigidbody rb)) {
                rb.useGravity = true;
            }

            Debug.Log($"Revolver Released by: {ctx.interactorObject}");

        }

        // Function called when trigger pull
        private void FireBullet()
        {
            GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.transform.rotation);

            if (bullet.TryGetComponent(out BulletHit bulletHit))
            {
                bulletHit.Init(GunData, Firepoint);
                Debug.Log($"{gameObject.name} Fired Bullet", gameObject);
            }
            else {
                Debug.Log($"Bullet does not have BulletHit component");
            }
        }
    }
}
