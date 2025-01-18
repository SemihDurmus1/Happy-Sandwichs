using UnityEngine;

namespace Ingredient
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public abstract class IngredientItemBase : MonoBehaviour//I guess theres no need to implement MonoBehaviour
    {
        [SerializeField] private ScriptableIngredientItem ingredientItem;
        public ScriptableIngredientItem IngredientItem => ingredientItem;

        [SerializeField] protected Rigidbody grabbableRigidbody;
        protected Transform grabPointTransform;
    }
}