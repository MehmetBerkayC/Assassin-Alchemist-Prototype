using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Recipes", fileName ="Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemContainer[] craftingItems;
    public ItemContainer product;
}