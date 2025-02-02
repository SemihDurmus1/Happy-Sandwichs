using Ingredient;
using Sandwich;
using System.Collections.Generic;
using UnityEngine;

namespace Customer.Order
{
    public class CustomerOrder
    {
        /// <summary>
        /// Sandwiches in a CustomerOrder
        /// </summary>
        [SerializeField] private List<SandwichItem> sandwichOrderList = new();

        /// <summary>
        /// (Property) Sandwiches in a CustomerOrder
        /// </summary>
        public List<SandwichItem> SandwichOrderList => sandwichOrderList;

        public void AddSandwichToOrder()
        {
            SandwichItem newSandwich = new();

            int ingredientCount = Random.Range(1, IngredientCenter.Instance.allIngredients.Length);//Define how many ingredients the sandwich will have

            AddIngredientToSandwich(newSandwich, ingredientCount);

            sandwichOrderList.Add(newSandwich);

            //return customerOrderList.SandwichOrderList;
        }

        private void AddIngredientToSandwich(SandwichItem newSandwich, int ingredientCount)
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                int randomIngredient = Random.Range(0, IngredientCenter.Instance.allIngredients.Length);
                newSandwich.ingredients.Add(IngredientCenter.Instance.allIngredients[randomIngredient]);
            }
        }

        private void AddSandwich(SandwichItem sandwich)
        {
            sandwichOrderList.Add(sandwich);
        }

        private void RemoveSandwich(SandwichItem sandwich)
        {
            sandwichOrderList.Remove(sandwich);
        }
    }
}