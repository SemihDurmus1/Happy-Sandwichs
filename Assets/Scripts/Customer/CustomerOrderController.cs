using Ingredient;
using Sandwich;
using UnityEngine;

namespace Customer.Order
{
    public class CustomerOrderController : MonoBehaviour
    {
        [SerializeField] private CustomerOrder customerOrder;

        public void GenerateOrder()
        {
            if (customerOrder == null)
            {
                customerOrder = new();
            }

            AddRandomSandwichesToOrder(customerOrder, Random.Range(1, 4) );

            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)
            {
                    string sandwichString = "Sandwich " + (i + 1) + " ingredients: ";
                for (int j = 0; j < customerOrder.SandwichOrderList[i].ingredients.Count; j++)
                {
                    sandwichString += customerOrder.SandwichOrderList[i].ingredients[j].name + ", ";
                }
                Debug.Log(sandwichString);
            }
        }

        public void AddRandomSandwichesToOrder(CustomerOrder customerOrderList, int sandwichAmount)
        {
            for (int i = 0; i < sandwichAmount; i++)
            {
                SandwichItem newSandwich = new();
                int ingredientCount = Random.Range(1, IngredientCenter.Instance.allIngredients.Length);//Define how many ingredients the sandwich will have
                AddIngredientToSandwich(newSandwich, ingredientCount);
                customerOrderList.SandwichOrderList.Add(newSandwich);
            }
        }

        private void AddIngredientToSandwich(SandwichItem newSandwich, int ingredientCount)
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                int randomIngredient = Random.Range(0, IngredientCenter.Instance.allIngredients.Length);
                newSandwich.ingredients.Add(IngredientCenter.Instance.allIngredients[randomIngredient]);
            }
        }
    }
}