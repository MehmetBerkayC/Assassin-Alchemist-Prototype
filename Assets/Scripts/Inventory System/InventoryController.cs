using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIInventoryPanel inventoryPanel;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialInventoryItems = new();

        [Header("Settings")]
        [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

        //public int InventorySize = 10;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryChanged += UpdateInventoryUI;
            foreach (InventoryItem item in initialInventoryItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
                //Debug.Log($"{item.Item.name}");
            }            
        }

        public InventorySO GetInventoryData() => inventoryData;

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryPanel.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryPanel.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Amount);
            }
        }

        private void PrepareUI()
        {
            inventoryPanel.InitializeInventoryUI(inventoryData.Size);
            inventoryPanel.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryPanel.OnSwapItems += HandleSwapItems;
            inventoryPanel.OnStartDragging += HandleDragging;
            inventoryPanel.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            // Operate action
            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                inventoryPanel.ShowItemAction(itemIndex);
                inventoryPanel.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }
           
            // Remove item from inventory
            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryPanel.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.Amount));
            }
        }

        private void DropItem(int itemIndex, int amount)
        {
            inventoryData.RemoveItem(itemIndex, amount);
            inventoryPanel.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            // Remove item from inventory
            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            // Operate action
            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                // Take notice of which game object you're sending
                itemAction.PerformAction(gameObject, inventoryItem.ItemState);
                // Debug.Log("item action performed");
                if (inventoryData.GetItemAt(itemIndex).IsEmpty) inventoryPanel.ResetSelection();
                // Debug.Log("empty item slot check after item action");
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;
            inventoryPanel.SetUpDraggableItem(inventoryItem.Item.ItemSprite, inventoryItem.Amount);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
            {
                inventoryPanel.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.Item;

            string description = PrepareDescription(inventoryItem);

            inventoryPanel.UpdateDescription(itemIndex, item.ItemSprite, item.name, description);
        }

        public string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.Item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.ItemState.Count; i++)
            {
                sb.Append($"{inventoryItem.ItemState[i].Parameter.ParameterName} : " +
                    $"{inventoryItem.ItemState[i].Value} / {inventoryItem.Item.DefaultParametersList[i].Value}");
                sb.AppendLine();
            }
            return sb.ToString();  
        }

        private void Update()
        {
            ToggleInventory();
        }

        private void ToggleInventory()
        {
            if (Input.GetKeyDown(inventoryToggleKey))
            {
                inventoryPanel.ToggleVisibility();

                if (inventoryPanel.gameObject.activeInHierarchy) // probably don't need it
                {
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryPanel.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Amount);
                    }
                }
            }
        }
    }
}