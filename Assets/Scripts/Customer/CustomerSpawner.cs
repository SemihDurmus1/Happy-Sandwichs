using System.Collections;
using UnityEngine;

namespace Customer {
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnTimer = 10f;

        private void Start()
        {
            spawnPoints = PointsManager.Instance.spawnPoints;
            StartCoroutine(SpawnCustomerRoutine());
        }

        private void SpawnCustomer()
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(customerPrefab, spawnPoints[randomPoint].position, Quaternion.identity);
        }

        IEnumerator SpawnCustomerRoutine()
        {
            Debug.Log("Routine Started");

            while (true)
            {
                yield return new WaitForSeconds(spawnTimer);
                SpawnCustomer();
            }
        }

        //private void OnDisable()
        //{
        //    StopAllCoroutines();
        //}

        //private void OnEnable()
        //{
        //    StartCoroutine(SpawnCustomerRoutine());
        //}
    }
}