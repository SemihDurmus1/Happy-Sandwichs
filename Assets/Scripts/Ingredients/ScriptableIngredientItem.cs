using UnityEngine;

namespace Ingredient
{
    [CreateAssetMenu(menuName = "Ingredients/Ingredient Item")]
    public class ScriptableIngredientItem : ScriptableObject
    {
        [SerializeField]private AbstractIngredientItemBase ingredientPrefab;//saves prefab
        //[SerializeField]private ScriptableIngredientItem[] compatibleIngredients;
        //[SerializeField]private ScriptableIngredientItem[] incompatibleIngredients;

        public AbstractIngredientItemBase IngredientPrefab => ingredientPrefab;

        //public bool IsCompatibleWith(ScriptableIngredientItem otherIngredient)
        //{
        //    if (otherIngredient == null) return false;

        //    if (System.Array.Exists(incompatibleIngredients, ingredient => ingredient == otherIngredient))
        //    {
        //        return false; // Eðer uyumsuz yiyecekler arasýnda varsa, uyumsuzdur.
        //    }

        //    return System.Array.Exists(compatibleIngredients, ingredient => ingredient == otherIngredient);
        //}

        //public bool IsCompatibleWith(FoodItem otherFood)
        //{
        //    if (otherFood == null) { return false; }

        //    return System.Array.Exists(compatibleFoods, food => food == otherFood);

        //    foreach (FoodItem compatible in compatibleFoods)
        //    {
        //        if (compatible == otherFood)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;

        //}





    }
}