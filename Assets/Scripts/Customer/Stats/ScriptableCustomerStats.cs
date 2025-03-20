using UnityEngine;

namespace Customer.Stats
{
    [CreateAssetMenu(menuName = "Customer/Customer Stats")]
    public class ScriptableCustomerStats : ScriptableObject
    {
        [Range(0f, 1f)]
        [SerializeField] private float angerRate, patience, stinginess, rudeness;

        private ScriptableCustomerStats()
        {
            InitializeRandomStats();
        }

        private void InitializeRandomStats()
        {

        }
    }
}