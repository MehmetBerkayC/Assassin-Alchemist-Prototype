using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory System/Inventory", fileName = "new InventorySO")]
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

        public int AddItem(ItemSO item, int amountToAdd, List<ItemParameter> itemState = null)
        {
            if (!item.IsStackable)
            {
                while (amountToAdd > 0 && !IsInventoryFull())
                {
                    amountToAdd -= AddItemToFirstEmptySlot(item, 1, itemState);
                }
                InformAboutChange();
                return amountToAdd;
            }
            amountToAdd = AddStackableItem(item, amountToAdd);
            InformAboutChange();
            return amountToAdd;
        }

        private int AddItemToFirstEmptySlot(ItemSO item, int amount, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                Item = item,
                Amount = amount,
                ItemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList: itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return amount;
                }
            }
            return 0;
        }

        // func -> return (Is any of the items(slots) are empty == false)
        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int amount)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;

                if (inventoryItems[i].Item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].Item.MaxStackSize - inventoryItems[i].Amount;

                    if (amount > amountPossibleToTake) // Item amount over stack limit
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeAmount(inventoryItems[i].Item.MaxStackSize);
                        amount -= amountPossibleToTake;
                    }
                    else // Item has enough space in stack
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeAmount(inventoryItems[i].Amount + amount);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            // Add leftover items
            while (amount > 0 && !IsInventoryFull())
            {
                int newAmount = Mathf.Clamp(amount, 0, item.MaxStackSize);
                amount -= newAmount;
                AddItemToFirstEmptySlot(item, newAmount);
            }

            // return any leftover item
            return amount;
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Amount);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            // index - value list that is modifiable by any class,
            // this is to protect real inventory items from being accessed from any class
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

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

        public void SwapItems(int itemIndex_1, int itemIndex_2)
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

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty) return;

                int remainder = inventoryItems[itemIndex].Amount - amount;
                if (remainder <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeAmount(remainder);
                }

                InformAboutChange();
            }
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