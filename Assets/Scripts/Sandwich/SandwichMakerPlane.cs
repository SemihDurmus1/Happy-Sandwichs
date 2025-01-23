using Ingredient;
using UnityEngine;

namespace Sandwich
{
    /// <summary>
    /// This class for manage the Sandwich Maker Planes
    /// </summary>
    public class SandwichMakerPlane : MonoBehaviour
    {
        [SerializeField] private LayerMask ingredientLayers;
        [SerializeField] private SandwichItem sandwich;
        [SerializeField] private IngredientItem[] ingredientItems;

        private void Start()
        {
            sandwich = new SandwichItem();
        }

        private void OnCollisionEnter(Collision other)
        {
            //AddIngredientToPlane(other);
        }
        private void OnCollisionExit(Collision other)
        {
            //RemoveIngredientFromPlane(other);
        }

        public void AddIngredientToPlane(IngredientItem ingredient)
        {
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)
            {
                //IngredientItem ingredientItem = ýngredient.gameObject.GetComponent<IngredientItem>();

                SandwichManager.Instance.AddIngredient(sandwich, ingredient.ScriptableIngredientItem);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }

        private void RemoveIngredientFromPlane(IngredientItem ingredient)
        {
            if ((ingredientLayers.value & (1 << ingredient.gameObject.layer)) != 0)
            {
                //IngredientItem ingredientItem = ingredient.gameObject.GetComponent<IngredientItem>();

                SandwichManager.Instance.RemoveIngredient(sandwich, ingredient.ScriptableIngredientItem);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }
    }
}