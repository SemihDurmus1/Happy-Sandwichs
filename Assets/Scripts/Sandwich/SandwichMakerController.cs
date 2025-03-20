using Customer.Order;
using Player;
using UnityEngine;

namespace Sandwich
{
    public class SandwichMakerController : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInteract();
            }
        }

        private void HandleInteract()
        {
            if (playerManager.currentGrabbable != null)//If we held something
            {
                if (playerManager.currentGrabbable is ResultSandwich resultSandwich)//If the object is a Sandwich
                {
                    if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
                        out RaycastHit raycastHit, playerManager.pickUpDistance, playerManager.NPCLayer))
                    {
                        OrderController orderController = raycastHit.transform.gameObject.GetComponent<OrderController>();
                        orderController.CompareOrder((ResultSandwich)playerManager.currentGrabbable);
                    }
                }
                else if (playerManager.currentSandwichPlane != null)//develop UI or red stroke around ingredient,maybe a sound
                {
                    Debug.Log("Place the item on the plane(Press F)");
                }
                else
                {
                    Debug.Log("Drop the object you holding");
                }
            }
            else if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
                     out RaycastHit raycastHit, playerManager.pickUpDistance, playerManager.sandwichPlaneLayer))//if ray hits a SandwichMakerPlane
            {
                if (raycastHit.transform.TryGetComponent<SandwichMakerPlane>(out playerManager.currentSandwichPlane))
                {
                    if (playerManager.currentSandwichPlane.IngredientItems.Count > 0)
                    {
                        playerManager.currentGrabbable = playerManager.currentSandwichPlane.PrepareSandwich();
                        playerManager.currentGrabbable.Grab(playerManager.grabPoint);
                        playerManager.currentSandwichPlane = null;
                    }
                    else
                    {
                        Debug.Log("Sandwich Plane is empty! ");
                    }
                }
            }
        }


     

    }
}