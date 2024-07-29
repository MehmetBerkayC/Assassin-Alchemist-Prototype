using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICraftingItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText; 
    [SerializeField] TextMeshProUGUI itemAmountText;

    public CraftingItemContainer CraftingItem = new();
    
    // One func to send item from Selectable(PlayerInventory in UI) TO Selected(Crafting Slots)
    public void Remove_FromCraftingItemSlot()
    {
        if(CraftingItem.ItemData != null)
        {
            Update_ItemData(CraftingItem, insertOperation: false);
        }
    }

    // One func to send item from Selected(Crafting Slots) TO Selectable(PlayerInventory in UI)
    public bool Insert_ToItemSlot(CraftingItemContainer craftingItem)
    {
        if(CraftingItem.ItemData == null && craftingItem.ItemData != null) {
            Update_ItemData(craftingItem, insertOperation: true);
            return true;
        }
        return false;
    }

    private void Update_ItemData(CraftingItemContainer item, bool insertOperation)
    {
        if(insertOperation)
        {
            CraftingItem.ItemData = item.ItemData;
            CraftingItem.Amount = item.Amount;
        }
        else
        {
            UICraftingSystem.Instance.Remove_SelectedItemFromCraft(item);
            CraftingItem.ItemData = null;
            CraftingItem.Amount = 0;
        }
        Update_ItemSlotInformation();
    }

    public void Update_ItemSlotInformation()
    {
        if (CraftingItem.ItemData != null)
        {
            itemNameText.text = CraftingItem.ItemData.ItemName;
            itemAmountText.text = CraftingItem.Amount.ToString();
        }
        else
        {
            itemNameText.text = "";
            itemAmountText.text = 0.ToString();
        }
    }
}
