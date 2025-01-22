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
        [SerializeField] private LayerMask sandwichMakerPlaneLayerMask;

        [SerializeField] private GrabbableObjectBase currentGrabbableObject;
        [SerializeField] private bool isGrabKeyHolding = false;
        
        private void Update()
        {
            PreviewOnSandwichMakerPlane();
            HandleGrabbing();
        }


        private void PreviewOnSandwichMakerPlane()// This method previews ingredients on SandwichMakerPlane when the raycast hits
        {
            if (currentGrabbableObject != null)//if we got something on hand
            {
                if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                    out RaycastHit raycastHit, pickUpDistance, sandwichMakerPlaneLayerMask))
                {
                    currentGrabbableObject.transform.position = raycastHit.transform.position;
                    currentGrabbableObject.transform.rotation = raycastHit.transform.rotation;
                    if (inputCenter.IsGrabKeyPressed && !isGrabKeyHolding)
                    {
                        isGrabKeyHolding = true;

                        Debug.Log("currentGrabbableObject");
                        currentGrabbableObject.Drop();
                        currentGrabbableObject = null;
                    }
                    else if (!inputCenter.IsGrabKeyPressed)//It allows interact when the key relased
                    {
                        isGrabKeyHolding = false;
                    }
                }
            }
        }

        private void HandleGrabbing()
        {
            if (inputCenter.IsGrabKeyPressed && !isGrabKeyHolding)//When press the grab key
            {
                isGrabKeyHolding = true;
                if (currentGrabbableObject != null)//if we got something on hand
                {
                    currentGrabbableObject.Drop();
                    currentGrabbableObject = null;
                }
                else if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                         out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out currentGrabbableObject))
                    {
                        currentGrabbableObject.Grab(grabPointTransform);
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