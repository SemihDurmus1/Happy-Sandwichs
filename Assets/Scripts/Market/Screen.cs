using Player.Input;
using UnityEngine;

namespace Market
{
    public class Screen : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoxCollider screenCollider;
        [SerializeField] private PlayerInputCenter playerInputCenter;
        private void Start()
        {
            screenCollider = GetComponent<BoxCollider>();
            //playerInputCenter = FindAnyObjectByType<PlayerInputCenter>();
        }
        public void ShowInteractUI()
        {
            Debug.Log("Screen interact");
            GameManager.Instance.uiManager.ActivateUI(GameManager.Instance.uiManager.clickUI);
        }

        public void HideInteractUI()
        {
            GameManager.Instance.uiManager.DeactivateUI(GameManager.Instance.uiManager.clickUI);
        }

        public void Interact()
        {
            GameManager.Instance.cursorManager.ToggleCursor(true);

            screenCollider.enabled = false;

            //playerInputCenter = FindAnyObjectByType<PlayerInputCenter>();
            playerInputCenter.SetInputEnabled(false); // disable inputs

        }

        public void DeInteract()
        {
            GameManager.Instance.cursorManager.ToggleCursor(false);

            screenCollider.enabled = true;

            playerInputCenter.SetInputEnabled(true); // enable inputs
        }
    }
}