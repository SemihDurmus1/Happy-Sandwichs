using Ingredient;
using Player;
using UnityEngine;

public class Market : MonoBehaviour
{
    //Maybe this can be a Singleton
    private void BuyIngredient(ScriptableIngredientItem ingredient, int quantity)
    {
        PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
        playerManager.wallet.RemoveFromWallet(ingredient.buyPrice * quantity);
    }
}
