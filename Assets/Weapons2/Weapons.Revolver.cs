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
        public PlayerInput PlayerInput;
        private Rogue.UpgradeManager _upgradeManager;

        public string WhichHand;




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

            if (gameObject.TryGetComponent(out PlayerInput playerInput))
            {
                PlayerInput = playerInput;
            }
            else
            {
                Debug.Log("No Player Input Component", gameObject);
            }

            _upgradeManager = UpgradeManager.FindLive(gameObject);
        }

        // These functions are triggered by the Select Enter/Exit events on the XR Grab Interactable component
        // The Select Enter/Exit are triggered (only once) when grabbed or released
        public void OnGrab(SelectEnterEventArgs ctx)
        {
            // Toggling the PlayerInput on/off does what the InHand check did, prevents shooting when not in hand
            /*PlayerInput.enabled = true;*/
            // Roundabout way to get the name of which controller grabbed this (no direct gameobject access, had to use transform)
            WhichHand = ctx.interactorObject.transform.gameObject.name;
            Debug.Log($"Revolver Grabbed by: {ctx.interactorObject}");
            Debug.Log($"Revolver Input: {PlayerInput.enabled}");
        }
        public void OnRelease(SelectExitEventArgs ctx)
        {
            /*PlayerInput.enabled = false;*/
            WhichHand = "";
            Debug.Log($"Revolver Released by: {ctx.interactorObject}");
            Debug.Log($"Revolver Input: {PlayerInput.enabled}");
        }

        // This function is triggered by the Right/Left Activate actions in the Input Map
        // Default is Right/Left controllers both call an Activate function, renamed so it can differentiate
        public void OnFire(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValueAsButton())
            {
                // If hand matches the trigger
                if (WhichHand == "Right Controller" && ctx.action.name == "Right Activate")
                {
                    Debug.Log("Right Pew");
                    FireBullet();
                }
                else if (WhichHand == "Left Controller" && ctx.action.name == "Left Activate")
                {
                    Debug.Log("Left Pew");
                    FireBullet();
                }
            }
        }

        private void FireBullet()
        {
            GameObject bullet = Instantiate(BulletPrefab, Firepoint.position, BulletPrefab.transform.rotation);

            if (bullet.TryGetComponent(out Rigidbody bulletRb))
            {
                bulletRb.velocity = Firepoint.forward * GunData.Stats.Find(stat => stat.Key == StatKey.Speed).FloatValue;
            }
        }
    }
}
