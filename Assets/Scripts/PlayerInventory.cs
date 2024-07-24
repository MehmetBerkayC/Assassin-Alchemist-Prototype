using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<CraftingItemContainer> craftingInventory = new List<CraftingItemContainer>();

    public void AddItemToInventory(CraftingItemContainer item)
    {
        if (craftingInventory.Contains(item)) {
            int itemIndex = craftingInventory.IndexOf(item);
            //craftingInventory[itemIndex]
        }
        craftingInventory.Add(item);
    }
}
