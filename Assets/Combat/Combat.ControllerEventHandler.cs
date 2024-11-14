using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Combat
{
    public class ControllerEventHandler : MonoBehaviour
    {
        public GameObject Holster;
        private Manager _combatManager;
        public UnityEvent OnTriggerPull;

        public void Start()
        {
            if (Holster)
            {
                _combatManager.Holsters[gameObject.name] = Holster;
            }
            else
            {
                Debug.Log($"No Holster set for {gameObject}", gameObject);
            }
        }

        public void HandleTriggerPull(InputAction.CallbackContext ctx)
        {
            //Debug.Log($"Input Ctx from {gameObject}: {ctx}");
            if (ctx.ReadValueAsButton())
            {
                Debug.Log($"HandleTriggerPull on {gameObject.name} called on {ctx.action.name}");
                OnTriggerPull.Invoke();
            }
        }

    }
}