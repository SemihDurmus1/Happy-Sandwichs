using Player;
using Sandwich;
using Unity.VisualScripting;
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
                        Debug.Log(raycastHit.collider.gameObject.name);
                        Destroy(playerManager.currentGrabbable.gameObject);
                    }
                }
                else if (playerManager.currentSandwichPlane != null)//develop UI or red stroke around ingredient,maybe a sound
                    Debug.Log("Cant drop it on there");
                else
                    Debug.Log("Drop the object you holding");
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


        private void AlternativeCode()
        {
            //This code isnt work stable. I tried to do it with one raycast but its not good in application
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
                out RaycastHit raycastHit, playerManager.pickUpDistance))//If ray hits something
                {
                    //If the object that raycast hits is in sandwichPlaneLayer
                    if ((playerManager.sandwichPlaneLayer.value & (1 << raycastHit.collider.gameObject.layer)) != 0)//Bitwise
                    {
                        playerManager.currentSandwichPlane = raycastHit.transform.GetComponent<SandwichMakerPlane>();
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
                    else if (raycastHit.collider.gameObject.layer == playerManager.NPCLayer)
                    {
                        //Give the sandwich to the NPC
                    }
                }
            }
        }

    }
}