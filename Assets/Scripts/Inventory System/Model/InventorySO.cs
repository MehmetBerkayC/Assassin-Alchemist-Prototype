using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory System/Inventory", fileName ="new Inventory")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryChanged;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();

            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public void AddItem(ItemSO item, int amount)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem
                    {
                        Item = item,
                        Amount = amount
                    };
                    return;
                }
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Amount);
        }

        public Dictionary<int , InventoryItem> GetCurrentInventoryState() {
            // index - value list that is modifiable by any class,
            // this is to protect real inventory items from being accessed from any class
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int , InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        internal void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryChanged?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem 
    {
        // Why struct?: we don't want the items to be accessible and modifiable by any class, new items have to be made
        // so we use immutable type struct to protect items
        // -> struct is a value-type, while classes and interfaces are reference-type
        public int Amount;
        public ItemSO Item;
        public bool IsEmpty => Item == null;

        public InventoryItem ChangeAmount()
        {
            return new InventoryItem
            {
                Item = this.Item,
                Amount = 0
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem { Item = null, Amount = 0};
    }
}
