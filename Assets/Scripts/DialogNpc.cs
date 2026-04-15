using Interfaces;
using ScriptableObjects.Dialog;
using UnityEngine;

public class DialogNpc : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogSectionData dialog;
    
    public void Interact() => DialogController.Instance.StartDialog(dialog);
}