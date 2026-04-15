using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject interactionTip;

    private InputAction _interactAction;
    private readonly List<IInteractable> _availableInteractions = new();
    
    private void Awake() => _interactAction = inputActions.FindAction("Interact");

    private void Update()
    {
        if (!_interactAction.WasPressedThisFrame() || _availableInteractions is null) return;

        if (_availableInteractions.Count == 0) return;
        
        _availableInteractions[0].Interact();
        interactionTip.SetActive(_availableInteractions.Count != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent(out IInteractable interactable)) return;
        
        _availableInteractions.Add(interactable);
        interactionTip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent(out IInteractable interactable)) return;

        _availableInteractions.Remove(interactable);
        interactionTip.SetActive(_availableInteractions.Count != 0);
    }
}