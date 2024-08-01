using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public ItemContainer InventoryItem = null;

    // One func to send item from Selectable(PlayerInventory in UI) TO Selected(Crafting Slots)
    public void Remove_FromInventoryItemSlot()
    {
        if (InventoryItem.ItemData != null)
        {
            Update_ItemData(InventoryItem, false);
        }
    }

    // One func to send item from Selected(Crafting Slots) TO Selectable(PlayerInventory in UI)
    public bool Insert_ToItemSlot(ItemContainer craftingItem)
    {
        if (InventoryItem.ItemData == null)
        {
            Update_ItemData(craftingItem, insertOperation: true);
            return true;
        }
        return false;
    }

    private void Update_ItemData(ItemContainer item, bool insertOperation)
    {
        if (insertOperation)
        {
            InventoryItem.ItemData = item.ItemData;
            InventoryItem.Amount = item.Amount;
        }
        else
        {
            ItemContainer newItem = new ItemContainer(item.ItemData, 1); // Send only 1 item

            // Check removal success
            if (UICraftingSystem.Instance.Add_SelectedItemToCraft(newItem)) {
                if(item.Amount == 1)
                {
                    InventoryItem.ItemData = null;
                    InventoryItem.Amount = 0;
                }
                else
                {
                    InventoryItem.Amount -= 1;
                }
            }
        }
        Update_ItemSlotInformation();
    }

    public void Update_ItemSlotInformation()
    {
        if (InventoryItem.ItemData != null)
        {
            itemNameText.text = InventoryItem.ItemData.name;
            itemAmountText.text = InventoryItem.Amount.ToString();
        }
        else
        {
            itemNameText.text = "";
            itemAmountText.text = 0.ToString();
        }
    }
}