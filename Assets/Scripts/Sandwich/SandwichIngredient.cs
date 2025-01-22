using Ingredient;

namespace Sandwich
{
    [System.Serializable]
    public struct SandwichIngredient
    {
        public ScriptableIngredientItem Ingredient;
        //public int Amount;

        public SandwichIngredient(ScriptableIngredientItem ingredient)
        {
            Ingredient = ingredient;
            //Amount = amount;
        }



    }
}