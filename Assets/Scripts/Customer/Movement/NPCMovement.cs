using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Customer.Movement
{
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform[] orderPoints;//I'll make it an array and the 0th index will be the order point
        //[SerializeField] private OrderController customerOrderController;
        //[SerializeField]private CustomerState customerState;

        [SerializeField] private CustomerManager customerManager;

        private bool isOrderCompleted = false;

        private void Start()
        {
            orderPoints = PointsManager.Instance.orderPoints;

            if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); }
            if (orderPoints != null) { MoveToTarget(orderPoints[0].position, CustomerState.Walking); }
            StartCoroutine(UpdateCoroutine());
        }

        /// <summary>
        /// Moves to target and sets a new State
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newState"></param>
        public void MoveToTarget(Vector3 target, CustomerState newState)
        {
            navMeshAgent.SetDestination(target);
            customerManager.CustomerState = newState;
        }

        private IEnumerator UpdateCoroutine()//The code is dirty as hell, need to clean it
        {
            while (true)
            {
                if (!navMeshAgent.pathPending && customerManager.CustomerState == CustomerState.Walking &&//Pathpending ile hata cozuldu
                    navMeshAgent.remainingDistance <= 0.01f)
                {
                    customerManager.orderController.GenerateOrder();
                    customerManager.CustomerState = CustomerState.WaitingForFood;
                }
                else if (isOrderCompleted)
                {
                    if (customerManager.CustomerState != CustomerState.Leaving)
                    {
                        Vector3 randomLeavePoint = PointsManager.Instance.leavePoints[Random.Range(0, 2)].position;
                        MoveToTarget(randomLeavePoint, CustomerState.Leaving);
                    }

                    if (!navMeshAgent.pathPending && customerManager.CustomerState == CustomerState.Leaving &&
                              navMeshAgent.remainingDistance <= 0.01f)
                    {//NPC deletes itself at the first frame after this code, need to fix it

                        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }

        public void OnOrderCompleted()
        {
            isOrderCompleted = true;
        }

    }
}