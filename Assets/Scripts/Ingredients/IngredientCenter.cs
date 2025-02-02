using System.Linq;
using UnityEngine;

namespace Ingredient
{
    public class IngredientCenter : MonoBehaviour
    {
        public static IngredientCenter Instance { get; private set; }
        public readonly ScriptableIngredientItem[] allIngredients;

        private void Awake()//Singleton
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ValidateIngredients();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void ValidateIngredients()
        {
            var duplicateIngredients = allIngredients
                .GroupBy(ingredient => ingredient.name)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            if (duplicateIngredients.Any())
            {
                Debug.LogError("Duplicate ingredients found: " + string.Join(", ", duplicateIngredients));
            }
        }


        public ScriptableIngredientItem GetIngredient(string name)
        {
            foreach (var ingredient in allIngredients)
            {
                if (ingredient.name == name)
                    return ingredient;
            }
            return null;
        }

    }
}