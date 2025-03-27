using Customer.Movement;
using Customer.Order;
using Customer.Stats;
using UnityEngine;

namespace Customer
{
    public enum CustomerState
    {
        Walking,
        WaitingForOrder,
        WaitingForFood,
        Leaving
    }
    public class CustomerManager : MonoBehaviour
    {
        public CustomerState CustomerState { get; set; }
        [SerializeField] private ScriptableCustomerStats customerStats;

        public NPCMovement nPCMovement;
        public OrderController orderController;

        private void Start()
        {
            //nPCMovement = GetComponent<NPCMovement>();
            //orderController = GetComponent<OrderController>();
            customerStats.InitializeRandomStats();
        }
    }
}