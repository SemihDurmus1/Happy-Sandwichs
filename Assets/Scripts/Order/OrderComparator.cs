using Ingredient;
using Sandwich;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Customer.Order
{
    public class OrderComparator : MonoBehaviour
    {
        /// <summary>
        /// Compares two orders to see if they are the same
        /// Returns true if they have the same ingredients
        /// </summary>
        public bool CompareSandwiches(SandwichItem expected, SandwichItem delivered)
        {
            if (expected == null || delivered == null)
            {
                Debug.LogError("One of the sandwiches is null.");
                return false;
            }

            List<ScriptableIngredientItem> expectedIngredients = expected.ingredients;
            List<ScriptableIngredientItem> deliveredIngredients = delivered.ingredients;
            if (deliveredIngredients[0] == IngredientCenter.Instance.breadPrefab)
            {
                var lastIndex = deliveredIngredients.Count - 1;
                if (deliveredIngredients[lastIndex] == IngredientCenter.Instance.breadPrefab)
                {
                    expectedIngredients.Insert(0, IngredientCenter.Instance.breadPrefab);
                    expectedIngredients.Add(IngredientCenter.Instance.breadPrefab);
                }
                else
                {
                    //last item isnt bread
                    Debug.Log("last item isnt bread");
                    return false;
                }
            }
            else
            {
                //first item isnt bread
                Debug.Log("first item isnt bread");
                return false;
            }
            if (expectedIngredients.Count != deliveredIngredients.Count)
            {//Countlar eslesmezse direkt falselama metodu
                Debug.Log("Ingredient count doesn't match.");
                return false;
            }

            for (int i = 0; i < expectedIngredients.Count; i++)
            {
                if (expectedIngredients[i].name != deliveredIngredients[i].name)
                {
                    Debug.Log("Ingredients don't match.");
                    return false;
                }
            }
            return true;
        }


    }
}