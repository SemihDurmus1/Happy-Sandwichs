using Ingredient;
using System.Collections.Generic;
using UnityEngine;

namespace Sandwich
{
    public class SandwichManager : MonoBehaviour
    {
        public static SandwichManager Instance { get; private set; }

        private void Awake()//Singleton
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //public SandwichItem CreateSandwich()
        //{
        //    Sandwich newSandwich = new Sandwich();
        //    sandwiches.Add(newSandwich);
        //    return newSandwich;
        //}

        public void AddIngredient(SandwichItem sandwich, ScriptableIngredientItem ingredientItem)
        {
            sandwich.ingredients.Add(new SandwichIngredient(ingredientItem));
            Debug.Log(ingredientItem.name + " ingredient added.");

        }
        public void RemoveIngredient(SandwichItem sandwich, ScriptableIngredientItem ingredientItem)
        {
            sandwich.ingredients.Remove(new SandwichIngredient(ingredientItem));
            Debug.Log(ingredientItem.name + "ingredient removed");
        }
        public void PrintIngredients(SandwichItem sandwich)
        {
            string ingredients = "";
            for (int i = 0; i < sandwich.ingredients.Count; i++)
            {
                ingredients += sandwich.ingredients[i].Ingredient.name + ":\t";
            }
            Debug.Log(ingredients);
        }





    }
}