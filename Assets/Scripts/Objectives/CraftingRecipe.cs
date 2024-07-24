using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Recipes", fileName ="Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public CraftingItemContainer[] craftingItems;
    public CraftingItemContainer product;
}