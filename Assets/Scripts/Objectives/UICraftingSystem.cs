using System.Collections.Generic;
using UnityEngine;

public class UICraftingSystem : MonoBehaviour
{
    public static UICraftingSystem Instance;

    [Header("Inventory Item Grid")]
    [SerializeField] GameObject inventoryItemSlotPrefab;

    [Header("Generated Inventory Item Buttons")]
    [SerializeField] GameObject inventoryGrid;
    [SerializeField] List<GameObject> inventoryItemSlots_Button;
    
    [Header("Crafting Area")]
    [SerializeField] GameObject craftingPanel;
    [SerializeField] UICraftingItemSlot[] craftingInputItemSlots;
    [SerializeField] UICraftingItemSlot craftingOutputItemSlot;

    private List<CraftingItemContainer> playerInventory;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {   // Still don't know to destroy either gameobj or component
            Destroy(gameObject);
        }
    }

    /// CRAFTING PANEL WORKFLOW
    /// Get inventory info                                                  *-
    /// Generate buttons that have specified item information contained     *-
    ///     - Make sure buttons are automatically generateable              *-
    ///     - Make sure the click function works                            --
    ///     - Make sure the parent is right                                 *-
    /// Keep all generated buttons in a list
    /// Display generated item slots (^ buttons)
    /// -- Add Working Tooltip --
    /// - Make sure crafting slots display and update properly
    /// Crafting button calls crafting system
    /// Respond is calculated
    /// ***

    public void Update_InventoryDisplay()
    {
        playerInventory = PlayerInventory.Instance.GetInventoryItems();
        
        inventoryItemSlots_Button.Clear(); // find a better way to sort-display inventory

        foreach (CraftingItemContainer item in playerInventory)
        {
            if (item != null)
            {
                // Make new item slot(button) in UI and assign the item information
                UIInventoryItemSlot itemSlotUI = Instantiate(inventoryItemSlotPrefab, inventoryGrid.transform).GetComponent<UIInventoryItemSlot>();
                itemSlotUI.Insert_ToItemSlot(item);
                inventoryItemSlots_Button.Add(itemSlotUI.gameObject);
            }
        }
    }

    public void Add_SelectedItemToCraft(CraftingItemContainer inventoryItem)
    {
        foreach (UICraftingItemSlot itemSlot in craftingInputItemSlots)
        {
            if(itemSlot.CraftingItem == null)
            {
                // Set item on UI
                itemSlot.Insert_ToItemSlot(inventoryItem);
                // Remove from inventory
                PlayerInventory.Instance.RemoveItemFromInventory(inventoryItem);
                Update_InventoryDisplay();
                return;
            }
        }
        Debug.Log("All item slots are full!");
    }

    public void Remove_SelectedItemFromCraft(CraftingItemContainer craftingItem)
    {
        PlayerInventory.Instance.AddItemToInventory(craftingItem);
    }

    public void TaggleCraftingPanelVisibility()
    {
        craftingPanel.SetActive(!craftingPanel.activeInHierarchy);
    }
}
