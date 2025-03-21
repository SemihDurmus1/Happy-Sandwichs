using Ingredient;
using Sandwich;
using TMPro;
using UnityEngine;

namespace Customer.Order
{
    /// <summary>
    /// Handles the generation of sandwich orders
    /// </summary>
    public class OrderController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI orderText;
        [SerializeField] private OrderComparator orderComparator;

        [SerializeField] private Order customerOrder;

        [SerializeField] private CustomerManager customerManager;

        [SerializeField] private int maxSandwichAmount, maxIngredientAmount;

        public void GenerateOrder()
        {
            customerOrder ??= new();//Create new if its null
            orderComparator ??= new();
            AddRandomSandwichesToOrder(customerOrder, Random.Range(1, maxSandwichAmount + 1));

            PrintOrder();
        }
        public void AddRandomSandwichesToOrder(Order order, int sandwichAmount)
        {
            for (int i = 0; i < sandwichAmount; i++)
            {
                SandwichItem orderSandwich = new();
                //Define how many ingredients the sandwich will have
                int ingredientCount = Random.Range(1, maxIngredientAmount + 1);

                AddRandomIngredientsToSandwich(orderSandwich, ingredientCount);//Add ingredients up to the ingredient count
                order.SandwichOrderList.Add(orderSandwich);
            }
        }

        /// <summary>
        /// Adds random ingredients to a sandwich
        /// </summary>
        /// <param name="newSandwich"></param>
        /// <param name="ingredientCount"></param>
        private void AddRandomIngredientsToSandwich(SandwichItem newSandwich, int ingredientCount)
        {
            SandwichManager.Instance.AddIngredient(newSandwich, IngredientCenter.Instance.breadItem);
            for (int i = 1; i <= ingredientCount; i++)
            {
                int randomIngredient = Random.Range(0, IngredientCenter.Instance.allIngredients.Length);
                SandwichManager.Instance.AddIngredient(newSandwich, IngredientCenter.Instance.allIngredients[randomIngredient]);
            }
            SandwichManager.Instance.AddIngredient(newSandwich, IngredientCenter.Instance.breadItem);
        }
        private void PrintOrder()
        {
            orderText.text = "";
            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)
            {
                string sandwichString = "Sandwich " + (i + 1) + ": ";
                for (int j = 0; j < customerOrder.SandwichOrderList[i].ingredients.Count; j++)
                {
                    if (customerOrder.SandwichOrderList[i].ingredients[j].ingredientID != IngredientID.Bread)
                    {
                        sandwichString += customerOrder.SandwichOrderList[i].ingredients[j].name + ", ";
                    }
                }
                orderText.text += sandwichString + "\n";
            }
            if (customerOrder.SandwichOrderList.Count <= 0)
            {
                orderText.text = "Adams�n Kardo";
                orderText.color = Color.green;
            }
        }

        public void CompareOrder(ResultSandwich resultSandwich)
        {
            if (orderComparator == null)
            {
                Debug.LogWarning("OrderComparator is not assigned.");
                return;
            }

            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)//Needs to loop this with given sandwich's length
            {
                bool isMatch = orderComparator.CompareSandwiches
                    (customerOrder.SandwichOrderList[i], resultSandwich.sandwichItem);

                if (isMatch)
                {
                    Debug.Log("Correct sandwich delivered!");
                    customerOrder.SandwichOrderList.RemoveAt(i);

                    if (customerOrder.SandwichOrderList.Count <= 0)
                    {
                        //Burada movePointlere yurume kodu yazmaliyim.
                        customerManager.nPCMovement.OnOrderCompleted();
                    }

                    PrintOrder();
                    Destroy(resultSandwich.gameObject);
                    return;
                    // Ba�ar� durumunda �d�l verme veya NPC davran���n� tetikleme
                }
            }

            Debug.Log("No sandwiches matched");

        }


    }
}