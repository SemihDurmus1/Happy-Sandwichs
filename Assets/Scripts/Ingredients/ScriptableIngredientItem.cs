using UnityEngine;

namespace Ingredient
{
    [CreateAssetMenu(menuName = "Ingredients/Ingredient Item")]
    public class ScriptableIngredientItem : ScriptableObject
    {
        //Maybe i add ingredientID here
        [SerializeField]private IngredientItem ingredientPrefab;//saves prefab

        public IngredientItem IngredientPrefab => ingredientPrefab;

    }
}