using UnityEngine;

namespace Player
{
    public interface IPayToWallet
    {
        public void AddToWallet(float amount);
    }
    [System.Serializable]
    public class Wallet : IPayToWallet
    {
        //[SerializeField] private string walletName;
        [SerializeField] private float walletBalance;
        public float GetBalance() => walletBalance;

        public void AddToWallet(float amount)
        {
            walletBalance += amount;
            Debug.Log("Update Balance: " + walletBalance.ToString("F2"));
        }

        public bool RemoveFromWallet(float amount)
        {
            if (walletBalance >= amount)
            {
                walletBalance -= amount;
                Debug.Log("Update Balance: " + walletBalance);
                return true;
            }
            else
            {
                Debug.Log("Not enough money");
                return false;
            }
        }
    }
}