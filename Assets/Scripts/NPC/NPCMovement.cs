using Customer.Order;
using UnityEngine;
using UnityEngine.AI;

namespace Customer.Movement
{
    public enum CustomerState
    {
        Walking,
        WaitingForOrder,
        WaitingForFood,
    }
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform orderPosition;
        [SerializeField] private CustomerOrderController customerOrderController;

        private CustomerState customerState;

        private void Start()
        {
            if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); }
            if (orderPosition != null)
            {
                MoveToTarget(orderPosition.position);
            }
        }

        public void MoveToTarget(Vector3 target)
        {
            navMeshAgent.SetDestination(target);
            customerState = CustomerState.Walking;
        }

        private void Update()
        {
            if (customerState == CustomerState.Walking &&
                navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                customerOrderController.GenerateOrder();
                customerState = CustomerState.WaitingForFood;
            }
        }
    }




}