using Ingredient;
using Player;
using Sandwich;
using UnityEngine;

namespace Grabbing
{
    public class ItemGrabController : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;

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
            if (playerManager.currentGrabbable is not IngredientItem ingredient) { return; }//return if nothing on hand

            if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
                out RaycastHit raycastHit, playerManager.pickUpDistance, playerManager.sandwichPlaneLayer))//if ray hits a SandwichMakerPlane
            {
                if (raycastHit.transform.TryGetComponent<SandwichMakerPlane>(out playerManager.currentSandwichPlane))
                {
                    playerManager.currentSandwichPlane.PositionOnSandwichMakerPlane((IngredientItem)playerManager.currentGrabbable);
                }
            }
            else if (playerManager.currentSandwichPlane != null)
            {
                playerManager.currentSandwichPlane = null;
            }
        }
        private void HandleGrabInput()//This code is unoptimized and dirty as hell, need to fix it!
        {
            
            if (playerManager.inputCenter.IsGrabKeyPressed && !playerManager.inputCenter.IsGrabKeyHolding)//When press the grab key
            {
                playerManager.inputCenter.IsGrabKeyHolding = true;
                HandleGrabAction();
            }
            else if (!playerManager.inputCenter.IsGrabKeyPressed)//It allows interact when the key relased
            {
                playerManager.inputCenter.IsGrabKeyHolding = false;
            }
        }
        private void HandleGrabAction()
        {
            if (playerManager.currentGrabbable != null)//if we got something on hand
            {
                if (playerManager.currentGrabbable is IngredientItem ingredient
                    && playerManager.currentSandwichPlane != null)//if it's an ingredient and previewing on a plane
                {

                    PlaceOnSandwichMakerPlane();//Place the ingredient on the plane
                }
                else
                {
                    playerManager.currentGrabbable.Drop();
                    playerManager.currentGrabbable = null;
                }
                //else if (playerManager.currentGrabbable is ResultSandwich resultSandwich)
                //{
                //    playerManager.currentGrabbable.Drop();
                //    playerManager.currentGrabbable = null;
                //}
            }
            else TryPickUpIngredient();
        }

        private void TryPickUpIngredient()
        {
            if (playerManager.currentGrabbable != null)//this not so necessary but it's good for guarantee
            {
                Debug.Log("U cant grab this while you holding item");
                return;
            }
            if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
                out RaycastHit raycastHit, playerManager.pickUpDistance, playerManager.pickUpLayers)) //if we got nothing on hand and raycast hits
            {
                if (raycastHit.transform.TryGetComponent<GrabbableObjectBase>(out playerManager.currentGrabbable))
                {
                    if (playerManager.currentGrabbable is IngredientItem ingredient)
                    {

                        if (ingredient.placedSandwichPlane != null)//If taken ingredient on a SandwichMakerPlane, remove from there
                        {
                            ingredient.placedSandwichPlane.RemoveIngredientFromPlane(ingredient);
                        }

                        ingredient.Grab(playerManager.grabPoint);
                    }
                    else if (playerManager.currentGrabbable is ResultSandwich resultSandwich)
                    {
                        resultSandwich.Grab(playerManager.grabPoint);
                    }
                }
            }
        }

        private void PlaceOnSandwichMakerPlane()
        {
            playerManager.currentGrabbable.ResetVelocity();

            playerManager.currentSandwichPlane.AddIngredientToPlane((IngredientItem)playerManager.currentGrabbable);

            playerManager.currentSandwichPlane = null;
            playerManager.currentGrabbable = null;
        }

        private void OnDrawGizmos()
        {
            if (playerManager.camTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(playerManager.camTransform.position, playerManager.camTransform.forward * playerManager.pickUpDistance);
            }
        }
    }
}