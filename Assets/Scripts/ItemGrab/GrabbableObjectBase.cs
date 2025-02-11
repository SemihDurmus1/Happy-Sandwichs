using UnityEngine;

namespace Ingredient
{
    public class GrabbableObjectBase : MonoBehaviour, Grabbing.IGrabbable
    {
        [SerializeField] protected float lerpSpeed = 10f;
        [SerializeField] protected Rigidbody grabbableRigidbody;
        protected Transform grabPointTransform;//Maybe i can reference this statically and remove from method parameters

        public float Height
        {
            get
            {
                if (TryGetComponent<Collider>(out var collider))
                {
                    return collider.bounds.size.y;
                }
                if (TryGetComponent<Renderer>(out var renderer))
                {
                    return renderer.bounds.size.y;
                }
                return 0f;
            }
        }
    

        private void Awake()
        {
            if (grabbableRigidbody == null)
            {
                grabbableRigidbody = GetComponent<Rigidbody>();
            }
        }

        public void Grab(Transform grabPointTransform)
        {
            grabbableRigidbody.useGravity = false;
            grabbableRigidbody.isKinematic = true;
            this.grabPointTransform = grabPointTransform;
        }
        public void Drop()
        {
            grabPointTransform = null;
            grabbableRigidbody.useGravity = true;
            grabbableRigidbody.isKinematic = false;
        }

        public void ResetVelocity()
        {
            if (grabbableRigidbody == null) return;

            grabbableRigidbody.isKinematic = false;

            grabbableRigidbody.linearVelocity = Vector3.zero;
            grabbableRigidbody.angularVelocity = Vector3.zero;

            grabbableRigidbody.isKinematic = true;
            grabbableRigidbody.useGravity = false;
            grabPointTransform = null;

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
}