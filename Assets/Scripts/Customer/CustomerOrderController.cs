using Ingredient;
using Sandwich;
using TMPro;
using UnityEngine;

namespace Customer.Order
{
    public class CustomerOrderController : MonoBehaviour
    {
        [SerializeField] private CustomerOrder customerOrder;

        [SerializeField] private TextMeshProUGUI orderText;

        public void GenerateOrder()
        {
            if (customerOrder == null) { customerOrder = new(); }

            AddRandomSandwichesToOrder(customerOrder, Random.Range(1, 4));

            PrintOrder();
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

        private void PrintOrder()
        {
            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)
            {
                string sandwichString = "Sandwich " + (i + 1) + ": ";
                for (int j = 0; j < customerOrder.SandwichOrderList[i].ingredients.Count; j++)
                {
                    sandwichString += customerOrder.SandwichOrderList[i].ingredients[j].name + ", ";
                }
                Debug.Log(sandwichString);
                orderText.text += sandwichString + "\n";
            }
        }




    }
}