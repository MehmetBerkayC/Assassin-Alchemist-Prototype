using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [SerializeField] private List<CraftingItemContainer> craftingInventory = new List<CraftingItemContainer>();
    [SerializeField] private int inventoryCapacity = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {   // just destroy the component or gameobj?
            Destroy(this);
        }

        craftingInventory.Capacity = inventoryCapacity;
    }

    public void AddItemToInventory(CraftingItemContainer item) // currently addition doesn't care about max stack size
    {
        if(craftingInventory.Count <= craftingInventory.Capacity) 
        {
            if (craftingInventory.Contains(item)) {
                int itemIndex = craftingInventory.IndexOf(item);
                craftingInventory[itemIndex].Amount += item.Amount;
            }
            else
            {
                craftingInventory.Add(item);
            }
        }
    }

    public bool RemoveItemFromInventory(CraftingItemContainer item) // currently removes only 1
    {
        if (craftingInventory.Count > 0 && craftingInventory.Contains(item)) { 
            int itemIndex = craftingInventory.IndexOf(item);
            if (craftingInventory[itemIndex].Amount > 0)
            {
                craftingInventory[itemIndex].Amount -= 1;
            }
            else
            {
                craftingInventory.Remove(item);
            }
            return true;
        }
        return false;
    }

    public List<CraftingItemContainer> GetInventoryItems()
    {
        return craftingInventory;
    }
}
