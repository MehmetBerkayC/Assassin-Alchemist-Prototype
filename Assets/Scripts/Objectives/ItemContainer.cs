using UnityEngine;

[System.Serializable]
public class ItemContainer
{
    public ItemData ItemData;

    public int Amount = 1;

    public ItemContainer(ItemData itemData = null, int amount = 1)
    {
        ItemData = itemData;
        Amount = amount;
    }
}
