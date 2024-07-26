using UnityEngine;

public enum ItemType
{
    A,
    B,
    C,
    D
}

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Items", fileName ="Crafting Item Data")]
public class CraftingItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    public string ItemDescription;
    public ItemType ItemStats; // Change into a stat system later

    [Header("Item Instancing")]
    public GameObject ItemPrefab;
    public Sprite ItemSprite;
}
