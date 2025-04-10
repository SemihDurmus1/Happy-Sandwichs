using Ingredient;
using UnityEngine;

public class IngredientDeliverer : MonoBehaviour
{
    [SerializeField] private Transform _deliverPoint;

    public void GetDeliver(ScriptableIngredientItem ingredient)
    {
        Instantiate(ingredient.IngredientPrefab, _deliverPoint);
    }
}
