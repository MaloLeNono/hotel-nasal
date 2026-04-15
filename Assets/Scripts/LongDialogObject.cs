using Interfaces;
using UnityEngine;

public class LongDialogObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] dialogLines;
    
    public void Interact() => DialogController.Instance.StartDialog(dialogLines);
}