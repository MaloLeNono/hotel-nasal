using System.Linq;
using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RemoveItems : MonoBehaviour
{
    [SerializeField] private ItemData[] itemsToRemove;

    private void Awake()
    {
        var collider2d = GetComponent<Collider2D>();
        if (!collider2d.isTrigger)
            Debug.LogWarning($"Colliders on {nameof(RemoveItems)} should be triggers");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        int itemRemovedCount = itemsToRemove.Select(item => PlayerInventory.Instance.RemoveItem(item)).Count(itemRemoved => itemRemoved);
        
        if (itemRemovedCount == 1)
            DialogController.Instance.StartDialog("J'ai perdu un objet...");
        else if (itemRemovedCount > 1)
            DialogController.Instance.StartDialog("J'ai perdu quelques objets...");
    }
}
