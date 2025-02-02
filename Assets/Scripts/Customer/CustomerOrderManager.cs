using Ingredient;
using Sandwich;
using UnityEngine;

namespace Customer.Order
{
    public class CustomerOrderManager : MonoBehaviour
    {
        [SerializeField] private CustomerOrder customerOrder = new();
        private ScriptableIngredientItem[] allIngredientsList;

        private void Start()
        {
            allIngredientsList = IngredientCenter.Instance.allIngredients;

            GenerateOrder();
        }
        private void GenerateOrder()
        {
            AddSandwichToOrder(customerOrder);

            for (int i = 0; i < customerOrder.SandwichOrderList[0].ingredients.Count; i++)
            {
                Debug.Log(customerOrder.SandwichOrderList[0].ingredients[i].name);
            }
        }

        public void AddSandwichToOrder(CustomerOrder customerOrderList)
        {
            SandwichItem newSandwich = new();

            int ingredientCount = Random.Range(1, allIngredientsList.Length);//Define how many ingredients the sandwich will have

            AddIngredientToSandwich(newSandwich, ingredientCount);

            customerOrderList.SandwichOrderList.Add(newSandwich);

            //return customerOrderList.SandwichOrderList;
        }

        private void AddIngredientToSandwich(SandwichItem newSandwich, int ingredientCount)
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                int randomIngredient = Random.Range(0, allIngredientsList.Length);
                newSandwich.ingredients.Add(allIngredientsList[randomIngredient]);
            }
        }
    }
}