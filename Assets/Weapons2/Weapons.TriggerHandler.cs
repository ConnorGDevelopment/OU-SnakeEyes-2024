using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class TriggerHandler : MonoBehaviour
    {
        public UnityEvent OnTrigger;

        public void HandleTrigger(InputAction.CallbackContext ctx) { OnTrigger.Invoke(); }
    }
}
