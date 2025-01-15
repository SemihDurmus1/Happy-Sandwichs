using UnityEngine;

namespace Grabbing
{
    public class GrabItem : MonoBehaviour
    {
        [SerializeField] private PlayerInputCenter inputCenter;

        [SerializeField] private Transform playerCamTransform;
        [SerializeField] private Transform grabPointTransform;

        [SerializeField] private float pickUpDistance = 1.0f;
        [SerializeField] private LayerMask pickUpLayerMask;

        private GrabbableObject grabbableObject;
        private bool grabKeyPressed = false;

        private void Update()
        {
            HandleGrabbing();
        }

        private void HandleGrabbing()
        {
            if (inputCenter.IsGrabbingSomething && !grabKeyPressed)
            {
                grabKeyPressed = true;
                if (grabbableObject != null)//if we got something on hand
                {
                    grabbableObject.Drop();
                    grabbableObject = null;
                }
                else if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out grabbableObject))
                    {
                        grabbableObject.Grab(grabPointTransform);
                    }
                }
            }
            if (!inputCenter.IsGrabbingSomething)//It allows interact when the key relased
            {
                grabKeyPressed = false;
            }
        }
    }
}