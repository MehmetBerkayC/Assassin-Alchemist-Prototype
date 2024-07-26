using System.Collections.Generic;
using UnityEngine;

public class UICraftingPanel : MonoBehaviour
{
    public static UICraftingPanel Instance;

    [Header("Selectable Item Grid (Inventory)")]
    [SerializeField] GameObject selectableItemPrefab;

    [Header("Generated Item Buttons")]
    [SerializeField] GameObject InventoryGrid;
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

    public void Update_InventoryDisplay()
    {
        playerInventory = PlayerInventory.Instance.GetInventoryItems();
        
        inventoryItemSlots_Button.Clear(); // find a better way to sort-display inventory

        foreach (CraftingItemContainer item in playerInventory)
        {
            if (item != null)
            {
                // Make new item slot(button) in UI and assign the item information
                UICraftingItemSlot itemSlotUI = Instantiate(selectableItemPrefab, InventoryGrid.transform).GetComponent<UICraftingItemSlot>();
                itemSlotUI.Insert_ToItemSlot(item);
                inventoryItemSlots_Button.Add(itemSlotUI.gameObject);
            }
        }
    }

    public void Add_SelectedItemToCraft(UICraftingItemSlot craftingItemSlot)
    {
        foreach (UICraftingItemSlot itemSlot in craftingInputItemSlots)
        {
            if(itemSlot.CraftingItem == null)
            {
                // Set item on UI
                itemSlot.Insert_ToItemSlot(craftingItemSlot.CraftingItem);
                // Remove from inventory
                PlayerInventory.Instance.RemoveItemFromInventory(craftingItemSlot.CraftingItem);
                return;
            }
        }
        Debug.Log("All item slots are full!");
    }

    public void TaggleCraftingPanelVisibility()
    {
        craftingPanel.SetActive(!craftingPanel.activeInHierarchy);
    }
}
