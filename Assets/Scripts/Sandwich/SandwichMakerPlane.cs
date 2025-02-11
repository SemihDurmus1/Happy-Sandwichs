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
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)
            {
                ingredient.placedSandwichPlane = this;

                SandwichManager.Instance.AddIngredient(sandwich, ingredient.ScriptableIngredientItem);

                ingredientItems.Add(ingredient);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }
        public void RemoveIngredientFromPlane(IngredientItem ingredient)
        {
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)
            {
                SandwichManager.Instance.RemoveIngredient(sandwich, ingredient.ScriptableIngredientItem);

                ingredient.placedSandwichPlane = null;
                ingredientItems.Remove(ingredient);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }

        private void ClearSandwichandItemLists()
        {
            sandwich.ingredients.Clear();
            ingredientItems.Clear();
        }

        public ResultSandwich PrepareSandwich()
        {
            GameObject sandwichParent = Instantiate(IngredientCenter.Instance.resultSandwichPrefab);
            sandwichParent.transform.position = transform.position;
            foreach (IngredientItem ingredient in ingredientItems)
            {
                if (ingredient != null)
                {
                    Destroy(ingredient.GetComponent<Rigidbody>());

                    ingredient.transform.SetParent(sandwichParent.transform);
                }
            }
            ClearSandwichandItemLists();
            return sandwichParent.GetComponent<ResultSandwich>();
        }
    }
}