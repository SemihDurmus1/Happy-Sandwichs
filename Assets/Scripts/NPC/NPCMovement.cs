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
        [SerializeField] private Transform orderPoint;
        [SerializeField] private OrderController customerOrderController;

        private CustomerState customerState;

        private void Start()
        {
            if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); }
            if (orderPoint != null)
            {
                MoveToTarget(orderPoint.position);
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