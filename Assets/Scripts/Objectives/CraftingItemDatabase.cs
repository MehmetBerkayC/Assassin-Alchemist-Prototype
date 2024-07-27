using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Item Database", fileName ="Crafting Item Database")]
public class CraftingItemDatabase : ScriptableObject
{
    public CraftingItemData[] CraftingItems;

    public CraftingItemData GenerateItem()
    {
        int itemIndex = Random.Range(0, CraftingItems.Length);
        return CraftingItems[itemIndex];
    }
}
