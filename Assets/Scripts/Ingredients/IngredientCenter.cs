using System.Linq;
using UnityEngine;

namespace Ingredient
{
    public class IngredientCenter : MonoBehaviour
    {
        public static IngredientCenter Instance { get; private set; }
        /// <summary>
        /// All ingredient objects in the runtime
        /// </summary>
        public ScriptableIngredientItem[] allIngredients;

        [SerializeField] public ScriptableIngredientItem breadPrefab;
        [SerializeField] public GameObject resultSandwichPrefab;

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

        private void ValidateIngredients()//This for checking if there are any duplicated ingredients
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