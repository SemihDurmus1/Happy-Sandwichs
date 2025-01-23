﻿using Ingredient;
using Movement;
using Sandwich;
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

        [SerializeField] private IngredientItem currentIngredient;
        [SerializeField] private bool isGrabKeyHolding = false;

        [SerializeField] private SandwichMakerPlane currentSandwichMakerPlane;//The sandwich maker plane that we raycasting
        [SerializeField] private bool isOnSandwichPlane = false;
        
        private void Update()
        {
            //if (currentGrabbableObject != null)//if we got something on hand
            //{
            //    PreviewOnSandwichMakerPlane();
            //}
            HandleGrabbing();
        }


        private void PreviewOnSandwichMakerPlane()// This method previews ingredients on SandwichMakerPlane when the raycast hits
        {
            if (currentIngredient == null) { return; }//skip this method if we got nothing on hand

            if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                out RaycastHit raycastHit, pickUpDistance, sandwichMakerPlaneLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<SandwichMakerPlane>(out currentSandwichMakerPlane))
                {
                    PositionOnSandwichMakerPlane(raycastHit);
                }
            }
            else
            {
                currentSandwichMakerPlane = null;
                isOnSandwichPlane = false;
            }
        }

        private void PositionOnSandwichMakerPlane(RaycastHit raycastHit)
        {
            currentIngredient.transform.SetPositionAndRotation(raycastHit.transform.position, raycastHit.transform.rotation);
            isOnSandwichPlane = true;
        }

        private void HandleGrabbing()
        {
            PreviewOnSandwichMakerPlane();
            if (inputCenter.IsGrabKeyPressed && !isGrabKeyHolding)//When press the grab key
            {
                isGrabKeyHolding = true;
                if (currentIngredient != null)//if we got something on hand
                {
                    if (isOnSandwichPlane)//If we put an ingredient on the sandwich plane
                    {
                        PlaceOnSandwichMakerPlane();
                    }
                    else
                    {
                        currentIngredient.Drop();
                        currentIngredient = null;
                    }
                }
                else if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward,
                         out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //if we got nothing on hand and raycast hits
                {
                    if (raycastHit.transform.TryGetComponent<IngredientItem>(out currentIngredient))
                    {
                        currentIngredient.Grab(grabPointTransform);
                    }
                }
            }
            else if (!inputCenter.IsGrabKeyPressed)//It allows interact when the key relased
            {
                isGrabKeyHolding = false;
            }
        }

        private void PlaceOnSandwichMakerPlane()
        {
            currentIngredient.ResetVelocity();

            if (currentSandwichMakerPlane != null)
            {
                currentSandwichMakerPlane.AddIngredientToPlane(currentIngredient);
            }

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