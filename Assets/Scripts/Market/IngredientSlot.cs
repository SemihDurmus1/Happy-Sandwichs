using Ingredient;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Market {
    public class IngredientSlot : MonoBehaviour
    {
        [SerializeField] private ScriptableIngredientItem _ingredient;
        public Image icon;
        public TMP_Text nameText;
        public TextMeshProUGUI priceText;

        public void SetSlotInfos(ScriptableIngredientItem ingredient)
        {
            _ingredient = ingredient;
            icon.sprite = ingredient.ingredientIcon;
            nameText.text = ingredient.name;
            priceText.text = ingredient.price.ToString();
        }

        public void BuyButtonClicked()
        {
            PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();

            bool isEnoughMoney = playerManager.wallet.RemoveFromWallet(_ingredient.buyPrice);

            if (isEnoughMoney)
            {
                IngredientDeliverer deliver = FindAnyObjectByType<IngredientDeliverer>();
                deliver.GetDeliver(_ingredient);

                Debug.Log(nameText.text + " satin alindi");
            }
            else
            {
                Debug.Log("Amk fakiri");
            }
        }
    }




}