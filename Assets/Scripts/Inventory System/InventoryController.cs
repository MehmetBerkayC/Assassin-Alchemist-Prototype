using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
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
            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
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

            inventoryPanel.UpdateDescription(itemIndex, item.ItemSprite, item.name, item.Description);
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