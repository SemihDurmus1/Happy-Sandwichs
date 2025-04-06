using Ingredient;
using System.Collections.Generic;
using UnityEngine;

namespace Sandwich
{
    /// <summary>
    /// This class for manage the Sandwich Maker Planes. 
    /// It handles correct positioning ingredients on the plane, adding and removing ingredients from the sandwich.
    /// </summary>
    public class SandwichMakerPlane : MonoBehaviour
    {
        [SerializeField] private LayerMask ingredientLayers;
        [SerializeField] private SandwichItem sandwich;
        [SerializeField] private List<IngredientItem> ingredientItems;
        public List<IngredientItem> IngredientItems { get { return ingredientItems; } }


        private void Awake()
        {
            sandwich = new SandwichItem();
            //ingredientItems = new List<IngredientItem>();
        }

        public void PositionOnSandwichMakerPlane(IngredientItem ingredient)//sets ingredient's position on the plane
        {
            if (ingredientItems.Count > 0)
            {
                IngredientItem lastIngredient = ingredientItems[ingredientItems.Count - 1];

                Vector3 newPosition = lastIngredient.transform.position;
                newPosition.y += lastIngredient.Height / 2 + ingredient.Height / 2;//set the new ingredient's position on top of the last ingredient

                ingredient.transform.SetPositionAndRotation(newPosition, transform.transform.rotation);
            }
            else
            {
                ingredient.transform.SetPositionAndRotation(transform.position, transform.rotation);
            }
        }
        public void AddIngredientToPlane(IngredientItem ingredient)
        {
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)//Bitwise
            {
                ingredient.placedSandwichPlane = this;

                SandwichManager.Instance.AddIngredient(sandwich, ingredient.ScriptableIngredientItem);

                ingredientItems.Add(ingredient);

                //SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }
        public void RemoveIngredientFromPlane(IngredientItem ingredient)
        {
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)//Bitwise
            {
                SandwichManager.Instance.RemoveIngredient(sandwich, ingredient.ScriptableIngredientItem);

                ingredient.placedSandwichPlane = null;
                ingredientItems.Remove(ingredient);

                //SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }

        public ResultSandwich PrepareSandwich()
        {
            GameObject sandwichParent = Instantiate(IngredientCenter.Instance.resultSandwichPrefab);

            ResultSandwich resultSandwich = sandwichParent.GetComponent<ResultSandwich>();
            resultSandwich.sandwichItem = new SandwichItem(sandwich);//Transfer the Sandwich Plane's ingredients to the resultSandwich

            sandwichParent.transform.position = transform.position;

            foreach (IngredientItem ingredient in ingredientItems)//Delete the rigidbodys
            {
                if (ingredient != null)
                {
                    Destroy(ingredient.GetComponent<Rigidbody>());

                    AddPrice(resultSandwich, ingredient);

                    ingredient.transform.SetParent(sandwichParent.transform);
                }
            }
            ClearSandwichandItemLists();
            Debug.Log("Sandwich Price: " + resultSandwich.sandwichPrice);
            return resultSandwich;
        }

        private static void AddPrice(ResultSandwich resultSandwich, IngredientItem ingredient)
        {
            resultSandwich.sandwichPrice += ingredient.ScriptableIngredientItem.price;//Add ingredient's price
        }

        private void ClearSandwichandItemLists()
        {
            sandwich.ingredients.Clear();
            ingredientItems.Clear();
        }





    }
}