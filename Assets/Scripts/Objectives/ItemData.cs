using UnityEngine;

public enum ItemType
{
    A,
    B,
    C,
    D
}

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Items", fileName ="Crafting Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    public string ItemDescription;
    public ItemType Item_Type; // Change into a stat system later

    [Header("Item Instancing")]
    public GameObject ItemPrefab;
    public Sprite ItemSprite;
}
