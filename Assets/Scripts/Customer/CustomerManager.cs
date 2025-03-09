using Customer.Movement;
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
        NPCMovement NPCMovement;

    }
}