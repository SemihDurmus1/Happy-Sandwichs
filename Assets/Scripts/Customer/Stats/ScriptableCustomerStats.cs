using UnityEngine;

namespace Customer.Stats
{
    [CreateAssetMenu(menuName = "Customer/Customer Stats")]
    public class ScriptableCustomerStats : ScriptableObject
    {
        [Range(0f, 1f)]
        [SerializeField] private float angerRate, patience, stinginess, rudeness;

        private void OnEnable()
        {
            InitializeRandomStats();
        }

        public void InitializeRandomStats()
        {
            angerRate  = Random.Range(0.0f, 1.0f);
            patience   = Random.Range(0.0f, 1.0f);
            stinginess = Random.Range(0.0f, 1.0f);
            rudeness   = Random.Range(0.0f, 1.0f);
            /*Debug.Log("Anger: " + angerRate + "\t---\t" + 
                      "Patience: " + patience + "\t---\t" +
                      "Stinginess: " + stinginess + "\t---\t" +
                      "Rudeness: " + rudeness);*/
        }
    }
}