using Customer.Order;
using UnityEngine;
using UnityEngine.AI;

namespace Customer.Movement
{
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform[] orderPoints;//I'll make it an array and the 0th index will be the order point
        [SerializeField] private OrderController customerOrderController;

        [SerializeField]private CustomerState customerState;

        private void Start()
        {
            orderPoints = PointsManager.Instance.orderPoints;

            if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); }
            if (orderPoints != null) { MoveToTarget(orderPoints[0].position, CustomerState.Walking); }
        }

        /// <summary>
        /// Moves the target and sets a new State
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newState"></param>
        public void MoveToTarget(Vector3 target, CustomerState newState)
        {
            navMeshAgent.SetDestination(target);
            customerState = newState;
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

        void OnOrderCompleted()
        {
            MoveToTarget(PointsManager.Instance.leavePoints[Random.Range(0, 2)].position, CustomerState.Leaving);
        }

    }
}