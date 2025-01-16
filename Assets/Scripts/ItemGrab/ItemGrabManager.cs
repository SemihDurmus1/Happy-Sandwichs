using Ingredient;
using Movement;
using UnityEngine;

namespace Grabbing
{
    public class ItemGrabManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputCenter inputCenter;

        [SerializeField] private Transform playerCamTransform;
        [SerializeField] private Transform grabPointTransform;

        [SerializeField] private float pickUpDistance = 1.0f;
        [SerializeField] private LayerMask pickUpLayerMask;

        [SerializeField] private GrabbableObject grabbableObject;
        [SerializeField] private bool isGrabKeyHolding = false;

        private void Update()
        {
            HandleGrabbing();
        }

        private void HandleGrabbing()
        {
            if (inputCenter.IsGrabKeyPressed && !isGrabKeyHolding)//When press the grab key
            {
                isGrabKeyHolding = true;
                if (grabbableObject != null)//if we got something on hand
                {
                    grabbableObject.Drop();
                    grabbableObject = null;
                }
                else if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                         out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out grabbableObject))
                    {
                        grabbableObject.Grab(grabPointTransform);
                    }
                }
            }
            else if (!inputCenter.IsGrabKeyPressed)//It allows interact when the key relased
            {
                isGrabKeyHolding = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (playerCamTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(playerCamTransform.position, playerCamTransform.forward * pickUpDistance);
            }
        }




    }
}