using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Inventory Settings")]
    [SerializeField] private List<Item> inventory;
    [SerializeField] private int inventoryCapacity = 20;

    [Header("Item Database")]
    [SerializeField] ItemDatabase itemDatabase;

    [Header("Debug")]
    [SerializeField] private bool debug = false;
    [SerializeField, Range(1, 15)] private int testItemAmount = 8;


    private void Awake()
    {
        Instance = this;

        SetInventorySettings();
    }

    private void SetInventorySettings()
    {
        inventory.Capacity = inventoryCapacity;
        inventory = new List<Item>();
    }

    private void Start()
    {
        if (debug) TEST_GenerateInventory();

        // UICraftingSystem.Instance.Update_InventoryDisplay();
    }

    private void TEST_GenerateInventory()
    {
        inventory.Clear();
        Debug.Log("Adding random items to player inventory for TESTING!");

        // FALLBACK: int generatedItemAmount = UnityEngine.Random.Range(3, inventoryCapacity/2);
        for (int i = 0; i < testItemAmount; i++) {
            Item generatedItem = new Item();
            generatedItem.ItemData = itemDatabase.GenerateItem();
            generatedItem.Amount = UnityEngine.Random.Range(1, 10);
            Add_ItemToInventory(generatedItem);
        }

        UICraftingSystem.Instance.Update_InventoryDisplay();
    }

    public void Add_ItemToInventory(Item item) // currently addition doesn't care about max stack size
    {
        if(inventory.Count <= inventory.Capacity) 
        {
            int index = Contains_Index(item);
            Debug.Log(index);
            if (index != -1)
            {
                inventory[index].Amount += item.Amount;
                return;
            }
            // No match, Add as a new item
            inventory.Add(item);
        }
    }

    private bool Contains(Item item) // Comparing data inside items
    {
        foreach(Item container in inventory)
        {
            if (container.ItemData == item.ItemData) return true;
        }
        return false;
    }
    
    private int Contains_Index(Item item) // Returns index
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemData == item.ItemData) return i;
        }
        return -1;
    }

    public bool RemoveOnce_ItemFromInventory(Item item)
    {
        int itemIndex = Contains_Index(item);
        if (inventory.Count > 0 && itemIndex > -1)
        {
            if (inventory[itemIndex].Amount > 0)
            {
                inventory[itemIndex].Amount -= 1;
                return true;
            }
        }
        return false;
    }


    public bool Remove_ItemFromInventory(Item item) // currently removes only 1
    {
        int itemIndex = Contains_Index(item);
        if (inventory.Count > 0 && itemIndex > -1)
        {
            inventory.RemoveAt(itemIndex);
            return true;
        }
        return false;
    }

    public List<Item> GetInventoryItems()
    {
        return inventory;
    }
}
