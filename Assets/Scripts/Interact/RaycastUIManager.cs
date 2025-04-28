using Player;
using UnityEngine;

public interface IInteractable
{
    void ShowInteractUI();
    void HideInteractUI();
    void Interact();
    void DeInteract();
}

public class RaycastUIManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public IInteractable currentInteractable;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameManager.Instance.uiManager;
    }

    private void Update()
    {
        if (Physics.Raycast(playerManager.camTransform.position, playerManager.camTransform.forward,
            out RaycastHit raycastHit, playerManager.pickUpDistance, playerManager.interactionLayer))
        {
            IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();

                //if interactable null  or interactable null and current arent null
            if (interactable == null)
            {
                ClearInteraction();
            }
            else if (interactable != null && interactable != currentInteractable)//If interactable arent the current
            {
                currentInteractable = interactable;
                interactable.ShowInteractUI();
            }

            //Interactable varsa ve currentInteractable buysa - bir sey yapma
            //Interactable varsa ve current bu degilse - currenta ata
            //Interactable yoksa - Clearla
            //Interabtable yoksa ama current varsa - Clearla
        }
        else
        {
            ClearInteraction();

        }
    }

    private void ClearInteraction()
    {
        if (currentInteractable != null)
        {
            uiManager.DeactivateUI(uiManager.clickUI);
            currentInteractable = null;
        }
    }
}
