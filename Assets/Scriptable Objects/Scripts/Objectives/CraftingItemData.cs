using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    A,
    B,
    C, 
    D
}

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Items", fileName ="Crafting Item")]
public class CraftingItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    public ItemType ItemType;

    [Header("Item Instancing")]
    public GameObject ItemPrefab;
    public Sprite ItemSprite;
}
