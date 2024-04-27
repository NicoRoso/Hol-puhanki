using System;
using UnityEngine;

public class DialogueChecker : MonoBehaviour
{
    private Outline _outline;

    private PlayerInput _playerInput;

    private bool isTriggering;

    public static Action OnTriggerd;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _playerInput = new PlayerInput();
       // _playerInput.DialogueActivator.Activated.performed += ctx => ToTriggerDialogue();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _outline.enabled = true;
            isTriggering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _outline.enabled = false;
            isTriggering = false;
        }
    }

    private void ToTriggerDialogue()
    {
        if (isTriggering) { OnTriggerd?.Invoke(); }
    }
}
