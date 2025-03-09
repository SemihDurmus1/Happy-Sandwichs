using Ingredient;
using Sandwich;
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
            customerOrder ??= new();//Create new if its null

            AddRandomSandwichesToOrder(customerOrder, Random.Range(1, 4));

            PrintOrder();
        }
        public void AddRandomSandwichesToOrder(Order order, int sandwichAmount)
        {
            for (int i = 0; i < sandwichAmount; i++)
            {
                SandwichItem orderSandwich = new();
                //Define how many ingredients the sandwich will have
                int ingredientCount = Random.Range(1, IngredientCenter.Instance.allIngredients.Length);

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
                    if (customerOrder.SandwichOrderList[i].ingredients[j].ingredientID != IngredientID.Bread)
                    {
                        sandwichString += customerOrder.SandwichOrderList[i].ingredients[j].name + ", ";
                    }
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
                    }

                    PrintOrder();
                    Destroy(resultSandwich.gameObject);
                    return;
                    // Baþarý durumunda ödül verme veya NPC davranýþýný tetikleme
                }
                else
                {
                    Debug.Log("isMatch == null, returned");
                }
            }
        }


    }
}