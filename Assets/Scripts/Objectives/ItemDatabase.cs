using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Item Database", fileName ="Crafting Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] Items;

    public ItemData GenerateItem()
    {
        int itemIndex = Random.Range(0, Items.Length);
        return Items[itemIndex];
    }
}
