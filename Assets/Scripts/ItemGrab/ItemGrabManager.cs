using Ingredient;
using Movement;
using Sandwich;
using UnityEngine;

namespace Grabbing
{
    public class ItemGrabManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputCenter inputCenter;
        [SerializeField] private bool isGrabKeyHolding = false;

        [SerializeField] private Transform playerCamTransform;
        [SerializeField] private Transform grabPointTransform;

        [SerializeField] private float pickUpDistance = 1.0f;
        [SerializeField] private LayerMask pickUpLayerMask;
        [SerializeField] private LayerMask sandwichMakerPlaneLayerMask;

        [SerializeField] private IngredientItem currentIngredient;
        [SerializeField] private SandwichMakerPlane currentSandwichMakerPlane;//The sandwich maker plane that we raycasting
        
        private void Update()
        {
            HandleGrabbing();
        }

        private void HandleGrabbing()
        {
            PreviewOnSandwichMakerPlane();
            HandleGrabInput();
        }
        private void PreviewOnSandwichMakerPlane()// This method previews ingredients on SandwichMakerPlane when the raycast hits
        {
            if (currentIngredient == null) { return; }//return if nothing on hand

            if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                out RaycastHit raycastHit, pickUpDistance, sandwichMakerPlaneLayerMask))//if ray hits a SandwichMakerPlane
            {
                if (raycastHit.transform.TryGetComponent<SandwichMakerPlane>(out currentSandwichMakerPlane))
                {
                    currentSandwichMakerPlane.PositionOnSandwichMakerPlane(currentIngredient);
                }
            }
            else if (currentSandwichMakerPlane != null)
            {
                currentSandwichMakerPlane = null;
            }
        }

        private void HandleGrabInput()//This code is unoptimized and dirty as hell, need to fix it!
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentIngredient != null)
                {
                    currentIngredient.Drop();
                    currentIngredient = null;
                }
                if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                    out RaycastHit raycastHit, pickUpDistance, sandwichMakerPlaneLayerMask))//if ray hits a SandwichMakerPlane
                {
                    if (raycastHit.transform.TryGetComponent<SandwichMakerPlane>(out currentSandwichMakerPlane))
                    {
                        currentSandwichMakerPlane.PrepareSandwich();
                    }
                }    
            }
            if (inputCenter.IsGrabKeyPressed && !isGrabKeyHolding)//When press the grab key
            {
                isGrabKeyHolding = true;
                HandleGrabAction();
            }
            else if (!inputCenter.IsGrabKeyPressed)//It allows interact when the key relased
            {
                isGrabKeyHolding = false;
            }
        }

        private void HandleGrabAction()
        {
            if (currentIngredient != null)//if we got something on hand
            {
                if (currentSandwichMakerPlane != null)//If we put an ingredient on the sandwich plane
                {
                    PlaceOnSandwichMakerPlane();
                }
                else
                {
                    currentIngredient.Drop();
                    currentIngredient = null;
                }
            }
            else TryPickUpIngredient();
        }

        private void TryPickUpIngredient()
        {
            if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                                 out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //if we got nothing on hand and raycast hits
            {
                if (raycastHit.transform.TryGetComponent<IngredientItem>(out currentIngredient))
                {
                    if (currentIngredient.onThatSandwichMakerPlane != null)//If taken ingredient on a SandwichMakerPlane, remove from there
                    {
                        currentIngredient.onThatSandwichMakerPlane.RemoveIngredientFromPlane(currentIngredient);
                    }

                    currentIngredient.Grab(grabPointTransform);
                }
            }
        }

        private void PlaceOnSandwichMakerPlane()
        {
            currentIngredient.ResetVelocity();

            currentSandwichMakerPlane.AddIngredientToPlane(currentIngredient);

            currentSandwichMakerPlane = null;
            currentIngredient = null;
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