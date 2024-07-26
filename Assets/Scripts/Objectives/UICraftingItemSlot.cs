using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICraftingItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText; 
    [SerializeField] TextMeshProUGUI itemAmountText;

    public CraftingItemContainer CraftingItem;
    
    private void Start()
    {
        Update_ItemSlotInformation();
    }

    // One func to send item from Selectable(PlayerInventory in UI) TO Selected(Crafting Slots)
    public void Remove_FromItemSlot()
    {
        if(CraftingItem != null)
        {
            PlayerInventory.Instance.AddItemToInventory(CraftingItem);
            CraftingItem = null;
        }
    }

    // One func to send item from Selected(Crafting Slots) TO Selectable(PlayerInventory in UI)
    public bool Insert_ToItemSlot(CraftingItemContainer craftingItem)
    {
        if(CraftingItem == null) { 
            SetItem(craftingItem);
            return true;
        }else
        {
            return false;
        }
    }

    public void SetItem(CraftingItemContainer craftingItem)
    {
        CraftingItem = craftingItem;
        Update_ItemSlotInformation();
    }

    public void Update_ItemSlotInformation()
    {
        itemNameText.text = CraftingItem.ItemData.ItemName;
        itemAmountText.text = CraftingItem.Amount.ToString();
    }
}
