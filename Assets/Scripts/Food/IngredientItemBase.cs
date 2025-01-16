using UnityEngine;

namespace Ingredient
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public abstract class IngredientItemBase : MonoBehaviour
    {
        [SerializeField] private ScriptableIngredientItem ingredientItem;
        public ScriptableIngredientItem IngredientItem => ingredientItem;

        [SerializeField] protected Rigidbody grabbableRigidbody;
        [SerializeField] protected float lerpSpeed = 10f;
        protected Transform grabPointTransform;
    }
}