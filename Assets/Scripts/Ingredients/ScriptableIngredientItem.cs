using UnityEngine;

namespace Ingredient
{
    public enum IngredientID
    {
        Bread, Cheddar, Cheese, Egg, Leaf, Meat
    }

    [CreateAssetMenu(menuName = "Ingredients/Ingredient Item")]
    public class ScriptableIngredientItem : ScriptableObject
    {
        [SerializeField] private IngredientItem ingredientPrefab;//saves prefab
        public IngredientID ingredientID;
        public float price;
        public float buyPrice;

        public IngredientItem IngredientPrefab => ingredientPrefab;

    }
}