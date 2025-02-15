using Ingredient;
using Sandwich;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Customer.Order
{
    /// <summary>
    /// Handles the generation sandwich orders
    /// </summary>
    public class OrderController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI orderText;
        [SerializeField] private OrderComparator orderComparator;

        [SerializeField] private Order customerOrder;

        public void GenerateOrder()
        {
            if (customerOrder == null) { customerOrder = new(); }

            AddRandomSandwichesToOrder(customerOrder, Random.Range(1, 4));

            PrintOrder();
        }
        public void AddRandomSandwichesToOrder(Order order, int sandwichAmount)
        {
            for (int i = 0; i < sandwichAmount; i++)
            {
                SandwichItem orderSandwich = new();
                int ingredientCount = Random.Range(1, IngredientCenter.Instance.allIngredients.Length);//Define how many ingredients the sandwich will have

                AddIngredientToSandwich(orderSandwich, ingredientCount);//Add ingredients up to the ingredient count
                order.SandwichOrderList.Add(orderSandwich);
            }
        }

        /// <summary>
        /// Adds random ingredients to a sandwich
        /// </summary>
        /// <param name="newSandwich"></param>
        /// <param name="ingredientCount"></param>
        private void AddIngredientToSandwich(SandwichItem newSandwich, int ingredientCount)
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                int randomIngredient = Random.Range(0, IngredientCenter.Instance.allIngredients.Length);
                //newSandwich.ingredients.Add(IngredientCenter.Instance.allIngredients[randomIngredient]);
                SandwichManager.Instance.AddIngredient(newSandwich, IngredientCenter.Instance.allIngredients[randomIngredient]);
            }
        }
        private void PrintOrder()
        {
            orderText.text = "";
            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)
            {
                string sandwichString = "Sandwich " + (i + 1) + ": ";
                for (int j = 0; j < customerOrder.SandwichOrderList[i].ingredients.Count; j++)
                {
                    sandwichString += customerOrder.SandwichOrderList[i].ingredients[j].name + ", ";
                }
                orderText.text += sandwichString + "\n";
            }
            if (customerOrder.SandwichOrderList.Count <= 0)
            {
                orderText.text = "Adamsýn Kardo";
                orderText.color = Color.green;
            }
        }

        public void CompareOrder(ResultSandwich resultSandwich)
        {
            if (orderComparator == null)
            {
                Debug.LogError("OrderComparator is not assigned.");
                return;
            }

            for (int i = 0; i < customerOrder.SandwichOrderList.Count; i++)
            {
                //Burada 
                bool isMatch = orderComparator.CompareSandwiches(customerOrder.SandwichOrderList[i], resultSandwich.sandwichItem);
                if (isMatch)
                {
                    Debug.Log("Correct sandwich delivered!");
                    Destroy(resultSandwich.gameObject);
                    customerOrder.SandwichOrderList.RemoveAt(i);
                    PrintOrder();
                    Destroy(resultSandwich.gameObject);
                    return;
                    // Baþarý durumunda ödül verme veya NPC davranýþýný tetikleme
                }
                else
                {
                    //Debug.Log("Incorrect sandwich delivered. Try again.");
                    // Hatalý teslimat durumunda ek aksiyonlar (örneðin, tekrar sipariþ verme)
                }
            }
            

            //Destroy(resultSandwich.gameObject);
        }


    }
}