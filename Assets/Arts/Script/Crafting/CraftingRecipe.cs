using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public string itemName; // The name of the resulting item
        public List<RecipeIngredient> ingredients; // List of ingredients required

        [System.Serializable]
        public class RecipeIngredient
        {
            public string itemName; // Name of the ingredient
            public int quantity; // Quantity required
        }
    }
}
