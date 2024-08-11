using UnityEngine;

[System.Serializable]
public class Item : IItemContainer
{
    public ItemData ItemData;

    public int Amount = 1;

    public Item(ItemData itemData = null, int amount = 1)
    {
        ItemData = itemData;
        Amount = amount;
    }

    ItemData IItemContainer.ItemData => ItemData;

    int IItemContainer.Amount => Amount;
}
