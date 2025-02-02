using UnityEngine;

namespace Customer
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform orderTransform;

        private void Update()
        {
            Vector3 targetPosition = new Vector3(orderTransform.position.x, transform.position.y, orderTransform.position.z);

            Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 100f);
            rb.MovePosition(lerpPosition);
        }
    }
}