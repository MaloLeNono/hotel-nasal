using Interfaces;
using UnityEngine;

public class DialogObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] textOptions;
    
    public void Interact() => DialogController.Instance.StartDialog(textOptions[Random.Range(0, textOptions.Length)]);
}