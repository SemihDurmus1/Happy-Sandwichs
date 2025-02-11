using Player;
using Sandwich;
using UnityEngine;

public class SandwichMakerController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerManager.currentGrabbable != null)
            {
                if (playerManager.currentSandwichPlane != null)//develop UI or red stroke around ingredient,maybe a sound
                    Debug.Log("Cant drop it on there");
                else
                    Debug.Log("U cant complete a sandwich while you holding an object");
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
