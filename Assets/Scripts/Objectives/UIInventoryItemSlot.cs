using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public CraftingItemContainer InventoryItem;

    // One func to send item from Selectable(PlayerInventory in UI) TO Selected(Crafting Slots)
    public void Remove_FromInventoryItemSlot()
    {
        if (InventoryItem != null)
        {
            UICraftingSystem.Instance.Add_SelectedItemToCraft(InventoryItem); // Removes item from inventory automatically
            InventoryItem = null;
        }
    }

    // One func to send item from Selected(Crafting Slots) TO Selectable(PlayerInventory in UI)
    public bool Insert_ToItemSlot(CraftingItemContainer craftingItem)
    {
        if (InventoryItem == null)
        {
            SetItem(craftingItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetItem(CraftingItemContainer craftingItem)
    {
        InventoryItem = craftingItem;
        Update_ItemSlotInformation();
    }

    public void Update_ItemSlotInformation()
    {
        itemNameText.text = InventoryItem.ItemData.ItemName;
        itemAmountText.text = InventoryItem.Amount.ToString();
    }
}