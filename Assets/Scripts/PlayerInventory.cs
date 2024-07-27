using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [SerializeField] private List<CraftingItemContainer> craftingInventory = new List<CraftingItemContainer>();
    [SerializeField] private int inventoryCapacity = 20;

    [Header("For Testing")]
    [SerializeField] CraftingItemDatabase itemDatabase;

    [SerializeField] int maxStackSize = 32;

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

    private void Start()
    {
        TEST_GenerateInventory();

        // UICraftingSystem.Instance.Update_InventoryDisplay();
    }

    private void TEST_GenerateInventory()
    {
        craftingInventory.Clear();
        Debug.Log("Adding random items to player inventory for TESTING!");

        int generatedItemAmount = Random.Range(3, inventoryCapacity/2);
        for (int i = 0; i < generatedItemAmount; i++) {
            CraftingItemContainer newItem = new CraftingItemContainer();
            newItem.ItemData = itemDatabase.GenerateItem();
            newItem.Amount = Random.Range(1, 10);
            AddItemToInventory(newItem);
        }

        UICraftingSystem.Instance.Update_InventoryDisplay();
    }

    public void AddItemToInventory(CraftingItemContainer item) // currently addition doesn't care about max stack size
    {
        if(craftingInventory.Count <= craftingInventory.Capacity) 
        {
            foreach(CraftingItemContainer craftingItem in craftingInventory)
            {
                if (IsItemMatchingAndStackable(item, craftingItem))
                {
                    break; // Match occured and inventory updated
                }
            }
            // No match, Add as a new item
            craftingInventory.Add(item);
        }
    }

    private bool IsItemMatchingAndStackable(CraftingItemContainer item, CraftingItemContainer craftingItem)
    {
        if (craftingItem.ItemData == item.ItemData && craftingItem.Amount < maxStackSize)
        {
            if (!IsOverStacking(item, craftingItem))
            {
                craftingItem.Amount += item.Amount;
                return true;
            }
        }
        return false;
    }

    private bool IsOverStacking(CraftingItemContainer item, CraftingItemContainer craftingItem)
    {
        int newItemAmount = craftingItem.Amount + item.Amount;
        if (newItemAmount > maxStackSize)
        {
            int remainder = newItemAmount % maxStackSize; // Remainder after stacking
            craftingItem.Amount = maxStackSize; // old item has max stack size

            // ** Forget remainder for now **
            //// make a new item
            //CraftingItemContainer newItemContainer = new CraftingItemContainer();
            //newItemContainer.ItemData = item.ItemData;
            //newItemContainer.Amount = remainder;
            //AddItemToInventory(newItemContainer); // recursive!
            return true;
        }
        return false;
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
