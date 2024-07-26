using UnityEngine;

[System.Serializable]
public class CraftingItemContainer
{
    public CraftingItemData ItemData;

    [Range(1,32)]
    public int Amount = 1;
}
