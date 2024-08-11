using UnityEngine;

public class CraftingSystem : MonoBehaviour // no need to make it a monobehaviour, send the database via function
{
    public bool Craft(Item[] craftingIngredients, CraftingRecipeDatabase recipeDatabase)
    {
        // Ingredients *MUST BE IN ORDER*, otherwise array equalization will display error
        foreach(CraftingRecipe recipe in recipeDatabase.craftingRecipes)
        {
            if (craftingIngredients == recipe.craftingItems)
            {
                Debug.Log("Ingredients match recipe: " + recipe);
                // Give item if true or just return null or crafted item data
                return true;
            }
        }
        return false;
    }
}