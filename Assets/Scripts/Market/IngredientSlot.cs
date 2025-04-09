using Ingredient;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TextMeshProUGUI priceText;

    public void SetSlotInfos(ScriptableIngredientItem ingredient)
    {
        icon.sprite = ingredient.ingredientIcon;
        nameText.text = ingredient.name;
        priceText.text = ingredient.price.ToString();
    }
}
