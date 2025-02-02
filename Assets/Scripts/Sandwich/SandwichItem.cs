using Ingredient;
using System;
using System.Collections.Generic;

namespace Sandwich
{
    [Serializable]
    public class SandwichItem
    {
        public List<ScriptableIngredientItem> ingredients = new();
    }
}