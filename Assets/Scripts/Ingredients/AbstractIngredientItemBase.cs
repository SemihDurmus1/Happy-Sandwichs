using UnityEngine;

namespace Ingredient
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public abstract class AbstractIngredientItemBase : ScriptableIngredientItem
    {

    }
}