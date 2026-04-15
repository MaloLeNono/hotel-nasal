using System.Collections.Generic;

public class Inventory<T>
{
    public readonly int MaxItems;
    public List<T> Items { get; } = new();

    public int Count => Items.Count;
    
    public Inventory(int size)
    {
        MaxItems = size;
    }

    public void AddItem(T data)
    {
        if (Items.Count >= MaxItems) return;
        
        Items.Add(data);
    }

    public void RemoveItem(T itemData)
    {
        if (!Items.Contains(itemData)) return;
        
        Items.Remove(itemData);
    }
}