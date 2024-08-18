using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIInventoryPanel inventoryPanel;

    [SerializeField]
    private InventorySO inventoryData;

    [Header("Settings")]
    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

    public int InventorySize = 10;

    private void Start()
    {
        PrepareUI();
        //inventoryData.Initialize(); // While testing, disable this line or inventory will reset itself while initialization
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
    }

    private void HandleDragging(int itemIndex)
    {
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
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
