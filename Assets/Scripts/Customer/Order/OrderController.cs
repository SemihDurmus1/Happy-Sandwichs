using Ingredient;
using Player;
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
        [SerializeField] private TMP_Text orderText;
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
            orderText.color = Color.white;
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
                //Compare phase-------------------------------------------------------------
                bool isMatch = orderComparator.CompareSandwiches
                    (customerOrder.SandwichOrderList[i], resultSandwich.sandwichItem);

                //React Phase-------------------------------------------------------------
                if (!isMatch)
                {
                    ReactBad();
                    continue; /*pass this iteration*/
                }

                //Success Phase-------------------------------------------------------------
                PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
                playerManager.wallet.AddToWallet(1);

                TakeTheSandwich(resultSandwich, i);
                // Baþarý durumunda ödül verme veya NPC davranýþýný tetikleme
            }


        }

        private void TakeTheSandwich(ResultSandwich resultSandwich, int i)
        {
            customerOrder.SandwichOrderList.RemoveAt(i);

            if (customerOrder.SandwichOrderList.Count <= 0)
            {
                customerManager.nPCMovement.OnOrderCompleted();
            }

            PrintOrder();
            Destroy(resultSandwich.gameObject);
        }

        private void ReactBad()
        {
            orderText.text = "Orrospu çocu bu ne?";
            orderText.color = Color.red;
            Invoke(nameof(PrintOrder), 1.5f);
        }
    }
}