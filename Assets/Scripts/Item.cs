using Interfaces;
using ScriptableObjects;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;
    
    public void Interact()
    {
        if (PlayerInventory.Instance.IsFull)
            DialogController.Instance.StartDialog("Mon inventaire est plein...");
        else
        {
            PlayerInventory.Instance.AddItem(data);
            DialogController.Instance.StartDialog($"+ \"{data.itemName}\" x1");
            Destroy(gameObject);
        }
    }
}