using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty inputTriggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        float triggerValue = inputTriggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
    }
}
