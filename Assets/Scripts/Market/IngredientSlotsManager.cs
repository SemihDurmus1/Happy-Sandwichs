using Ingredient;
using Market;
using UnityEngine;

public class IngredientSlotsManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private ScriptableIngredientItem[] _allIngredients;

    private void Start()
    {
        _allIngredients = IngredientCenter.Instance.allIngredients;
        LoadIngredientSlots();
    }

    private void LoadIngredientSlots()
    {
        CreateIngredientSlot(IngredientCenter.Instance.breadItem);
        foreach (ScriptableIngredientItem ingredient in _allIngredients)
        {
            CreateIngredientSlot(ingredient);
        }
    }

    private void CreateIngredientSlot(ScriptableIngredientItem ingredient)
    {
        Instantiate(_slotPrefab, gameObject.transform);
        IngredientSlot ingredientSlot = _slotPrefab.GetComponent<IngredientSlot>();
        ingredientSlot.SetSlotInfos(ingredient);
    }
}
