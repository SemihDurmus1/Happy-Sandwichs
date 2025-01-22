using Ingredient;
using UnityEngine;

namespace Sandwich
{
    /// <summary>
    /// This class for manage the Sandwich Maker Planes
    /// </summary>
    public class SandwichMakerPlane : MonoBehaviour
    {
        [SerializeField] private SandwichItem sandwich;
        [SerializeField] private LayerMask ingredientLayers;

        private void Start()
        {
            sandwich = new SandwichItem();
        }

        private void OnCollisionEnter(Collision other)
        {
            if ( ( ingredientLayers.value & (1 << other.gameObject.layer) ) != 0 )
            {
                //Get component that collides with SandwichPlane
                IngredientItem ingredientItem = other.gameObject.GetComponent<IngredientItem>();

                SandwichManager.Instance.AddIngredient(sandwich, ingredientItem.ScriptableIngredientItem);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if ((ingredientLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                IngredientItem ingredientItem = other.gameObject.GetComponent<IngredientItem>();

                SandwichManager.Instance.RemoveIngredient(sandwich, ingredientItem.ScriptableIngredientItem);

                SandwichManager.Instance.PrintIngredients(sandwich);
            }
        }


    }
}