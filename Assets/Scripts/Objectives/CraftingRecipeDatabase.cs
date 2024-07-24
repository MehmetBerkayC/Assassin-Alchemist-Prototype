using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Crafting/Crafting Recipe Database", fileName ="Crafting Recipe Database")]
public class CraftingRecipeDatabase : ScriptableObject
{
    public CraftingRecipe[] craftingRecipes;
}
