using System.Collections.Generic;
using System.Linq;
using Enums;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private int size;
    [SerializeField] private InputActionAsset inputActions;

    [Header("Rarity Colors")] [SerializeField]
    private Color commonColor;

    [SerializeField] private Color uncommonColor;
    [SerializeField] private Color rareColor;
    [SerializeField] private Color epicColor;
    [SerializeField] private Color legendaryColor;
    [SerializeField] private Color mythicalColor;
    [SerializeField] private Color godlikeColor;

    [Header("Config")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private Transform itemParent;

    private Inventory<ItemData> _inventory;
    private InputAction _toggleInventoryAction;
    private readonly List<GameObject> _itemViews = new();

    public List<ItemData> Items => _inventory.Items;
    public bool IsFull => _inventory.Count == size;

    protected override void Awake()
    {
        base.Awake();
        _inventory = new Inventory<ItemData>(size);
        _toggleInventoryAction = inputActions.FindAction("ToggleInventory");
    }

    private void Update()
    {
        if (_toggleInventoryAction.WasPressedThisFrame())
            canvas.enabled = !canvas.enabled;
    }

    public void AddItem(ItemData item)
    {
        if (_inventory.Count >= size)
            return;

        GameObject itemView = Instantiate(itemInfoPrefab, itemParent);
        _itemViews.Add(itemView);
        
        var itemText = itemView.GetComponent<TextMeshProUGUI>();
        var itemData = itemView.GetComponent<InventoryItemView>();
        itemData.item = item;
        itemText.text = $"[ {item.type.ToString().ToUpper()} ] - {item.itemName}";

        if (!itemData.item.sellable)
            itemText.text += " (INVENDABLE)";
        
        itemText.color = item.type switch
        {
            ItemType.Typique => commonColor,
            ItemType.Atypique => uncommonColor,
            ItemType.Rare => rareColor,
            ItemType.Épique => epicColor,
            ItemType.Légendaire => legendaryColor,
            ItemType.Mythique => mythicalColor,
            ItemType.Divin => godlikeColor,
            _ => itemText.color
        };
        
        _inventory.AddItem(item);
    }

    public bool RemoveItem(ItemData item)
    {
        GameObject itemViewToRemove = (
            from itemView in _itemViews
            where itemView 
            let view = itemView.GetComponent<InventoryItemView>()
            where view && view.item == item 
            select itemView
        ).FirstOrDefault();

        if (!itemViewToRemove) return false;
        
        _inventory.RemoveItem(item);
        _itemViews.Remove(itemViewToRemove);
        Destroy(itemViewToRemove);

        return true;
    }
}