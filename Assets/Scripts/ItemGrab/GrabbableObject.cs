using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody grabbableRigidbody;
    [SerializeField] private float lerpSpeed = 10f;
    private Transform grabPointTransform;

    private void Awake()
    {
        grabbableRigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform grabPointTransform)
    {
        grabbableRigidbody.useGravity = false;
        this.grabPointTransform = grabPointTransform;
    }

    public void Drop()
    {
        grabPointTransform = null;
        grabbableRigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (grabPointTransform != null)
        {
            Vector3 targetPosition = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            grabbableRigidbody.MovePosition(targetPosition);
        }
    }
}
