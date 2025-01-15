using UnityEngine;

namespace FoodItems
{
    public class FoodItem : ScriptableObject
    {
        private GameObject foodPrefab;
        private FoodItem[] compatibleFoods;
        private FoodItem[] inCompatibleFoods;
    }
}