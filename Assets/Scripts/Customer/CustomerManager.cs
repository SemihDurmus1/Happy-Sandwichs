using Customer.Movement;
using Customer.Order;
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
        public NPCMovement nPCMovement;
        public OrderController orderController;

        private void Start()
        {
            //nPCMovement = GetComponent<NPCMovement>();
            //orderController = GetComponent<OrderController>();
        }
    }
}