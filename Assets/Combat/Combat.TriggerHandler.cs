using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Combat
{
    public class TriggerHandler : MonoBehaviour
    {
        public UnityEvent OnTriggerPull;

        public void HandleTriggerPull(InputAction.CallbackContext ctx)
        {
            Debug.Log($"Input Ctx from {gameObject}: {ctx}");
            if (ctx.ReadValueAsButton())
            {
                Debug.Log($"HandleTriggerPull on {gameObject.name} called on {ctx.action.name}");
                OnTriggerPull.Invoke();
            }
        }
    }
}