using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryItemSlot : MonoBehaviour, IUIItemSlot
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public Item InventoryItem = null;

    // send item from PlayerInventory TO Crafting Slots
    public void Remove_FromItemSlot()
    {
        if (InventoryItem.ItemData != null)
        {
            Update_ItemData(InventoryItem, false);
        }
    }

    // send item from Crafting Slots TO PlayerInventory
    public bool Insert_ToItemSlot(Item craftingItem)
    {
        if (InventoryItem.ItemData == null)
        {
            Update_ItemData(craftingItem, insertOperation: true);
            return true;
        }
        return false;
    }

    private void Update_ItemData(Item item, bool insertOperation)
    {
        if (insertOperation)
        {
            InventoryItem.ItemData = item.ItemData;
            InventoryItem.Amount += item.Amount;
        }
        else
        {
            Item newItem = new Item(item.ItemData, 1); // Send only 1 item

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