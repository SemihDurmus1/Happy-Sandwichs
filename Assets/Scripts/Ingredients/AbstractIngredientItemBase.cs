using UnityEngine;

namespace Ingredient
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public abstract class AbstractIngredientItemBase : ScriptableIngredientItem
    {
        [SerializeField] private ScriptableIngredientItem scriptableIngredientItem;
        public ScriptableIngredientItem IngredientItem => scriptableIngredientItem;

    }
}