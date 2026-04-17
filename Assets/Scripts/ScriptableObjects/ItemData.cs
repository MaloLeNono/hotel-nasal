using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item Data")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int value;
        public ItemType type;
        public bool sellable = true;
    }
}