using Ingredient;
using Sandwich;
using System.Linq;
using UnityEngine;

namespace Customer.Order
{
    [System.Serializable]
    public class OrderComparator
    {
        //private ScriptableIngredientItem bread;
        //private void Start()
        //{
        //    //bread = IngredientCenter.Instance.breadPrefab;
        //}

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

            ////If the first and last ingredient of the expectedIngredient aren't bread
            //if (expected.ingredients[0] != IngredientCenter.Instance.breadItem && 
            //    expected.ingredients[^1]!= IngredientCenter.Instance.breadItem)
            //{
            //    expected.ingredients.Insert(0, IngredientCenter.Instance.breadItem);//Inserts the bread as first item and the array shifts one right
            //    expected.ingredients.Add(IngredientCenter.Instance.breadItem);
            //}

            if (expected.ingredients.Count != delivered.ingredients.Count)//return if lists counts doesn't match
            {
                Debug.Log("Ingredient counts doesn't match.");
                return false;
            }

            SortLists(expected, delivered);

            for (int i = 0; i < expected.ingredients.Count; i++)//Compare Sandwiches
            {
                if (expected.ingredients[i].ingredientID != delivered.ingredients[i].ingredientID)
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