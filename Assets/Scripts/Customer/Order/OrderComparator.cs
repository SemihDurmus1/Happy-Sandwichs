using Ingredient;
using Sandwich;
using System.Linq;
using UnityEngine;

namespace Customer.Order
{
    [System.Serializable]
    public class OrderComparator
    {
        /// <summary>
        /// Compares two orders to see if they are the same
        /// Returns true if they have the same ingredients
        /// </summary>
        public bool CompareSandwiches(SandwichItem expected, SandwichItem delivered)
        {
            if (expected == null || delivered == null)//Null check
            {
                Debug.LogWarning("One of the sandwiches are null."); return false;
            }

            //If our sandwich's first or last ingredient isn't bread
            if (delivered.ingredients[0] != IngredientCenter.Instance.breadItem ||
                delivered.ingredients[^1]!= IngredientCenter.Instance.breadItem)//^1 == list.Count - 1
            {
                Debug.Log("Put the breads correct!");//first or last ingredient isnt bread
                return false;
            }

            if (expected.ingredients.Count != delivered.ingredients.Count)//return if lists counts doesn't match
            {
                Debug.Log("Ingredient counts doesn't match.");
                return false;
            }
            
            SandwichItem expectedInstance = new(expected);
            SandwichItem deliveredInstance = new(delivered);

            SortLists(expectedInstance, deliveredInstance);

            for (int i = 0; i < expected.ingredients.Count; i++)//Compare Sandwiches
            {
                if (expectedInstance.ingredients[i].ingredientID != deliveredInstance.ingredients[i].ingredientID)
                {
                    Debug.Log("Ingredients doesn't match.");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sorts the ingredients by ID before comparing OrderSandwiches and DeliveredSandwich
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="delivered"></param>
        private static void SortLists(SandwichItem expected, SandwichItem delivered)
        {
            // Sorts the ingredients by ID before comparing
            expected.ingredients = expected.ingredients.OrderBy(ingredient => ingredient.ingredientID).ToList();
            delivered.ingredients = delivered.ingredients.OrderBy(ingredient => ingredient.ingredientID).ToList();
        }



    }
}