using Ingredient;
using System;
using System.Collections.Generic;

namespace Sandwich
{
    [Serializable]
    public class SandwichItem
    {
        public List<ScriptableIngredientItem> ingredients = new();

        /// <summary>
        /// Copy Constructor: Makes a new List and copy the sandwich's ingredients.
        /// </summary>
        /// <param name="sandwich"></param>
        public SandwichItem(SandwichItem sandwich)
        {
            ingredients = new List<ScriptableIngredientItem>(sandwich.ingredients);
        }

        // Default constructor
        public SandwichItem() { }
    }
}