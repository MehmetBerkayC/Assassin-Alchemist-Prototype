using System;
using System.Collections.Generic;

namespace Inventory.Model
{
    [Serializable]
    public struct InventoryItem
    {
        // Why struct?: we don't want the items to be accessible and modifiable by any class, new items have to be made
        // so we use immutable type struct to protect items
        // -> struct is a value-type, while classes and interfaces are reference-type
        public int Amount;

        public ItemSO Item;
        public List<ItemParameter> ItemState;
        public bool IsEmpty => Item == null;

        public InventoryItem ChangeAmount(int amount)
        {
            return new InventoryItem
            {
                Item = this.Item,
                Amount = amount,
                ItemState = new List<ItemParameter>(ItemState)
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem { 
            Item = null, 
            Amount = 0,
            ItemState= new List<ItemParameter>()
        };
    }
}