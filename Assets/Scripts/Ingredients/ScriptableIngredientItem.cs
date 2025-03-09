using UnityEngine;

namespace Ingredient
{
    public enum IngredientID
    {
        Bread,
        Cheddar,
        Cheese,
        Egg,
        Leaf,
        Meat,
    }

    [CreateAssetMenu(menuName = "Ingredients/Ingredient Item")]
    public class ScriptableIngredientItem : ScriptableObject
    {
        [SerializeField] public IngredientID ingredientID;
        //Maybe i add here uniqe ID declare system for ingredients

        [SerializeField] private IngredientItem ingredientPrefab;//saves prefab
        public IngredientItem IngredientPrefab => ingredientPrefab;

    }
}