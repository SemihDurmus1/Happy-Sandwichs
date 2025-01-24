using Sandwich;
using UnityEngine;

namespace Ingredient
{
    public class IngredientItem : GrabbableObjectBase
    {
        [SerializeField] private ScriptableIngredientItem scriptableIngredientItem;
        public ScriptableIngredientItem ScriptableIngredientItem => scriptableIngredientItem;

        public SandwichMakerPlane onThatSandwichMakerPlane;

        public float Height
        {
            get
            {
                Collider collider = GetComponent<Collider>();
                if (collider != null)
                {
                    return collider.bounds.size.y;
                }
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    return renderer.bounds.size.y;
                }
                return 0f;
            }
        }
    }
}