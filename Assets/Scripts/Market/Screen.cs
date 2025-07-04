using Player;
using Player.Camera;
using Player.Input;
using UnityEngine;

namespace Market
{
    public class Screen : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoxCollider screenCollider;
        [SerializeField] private PlayerManager playerManager;

        private void Start()
        {
            screenCollider = GetComponent<BoxCollider>();
            //playerInputCenter = FindAnyObjectByType<PlayerInputCenter>();
        }
        public void ShowInteractUI()
        {
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

            GameManager.Instance.cameraController.SetCamFocus(CameraFocusMode.Screen);

            playerManager.inputCenter.SetInputEnabled(false); // disable inputs

        }

        public void DeInteract()
        {
            GameManager.Instance.cursorManager.ToggleCursor(false);

            screenCollider.enabled = true;

            playerManager.inputCenter.SetInputEnabled(true); // enable inputs

            GameManager.Instance.cameraController.SetCamFocus(CameraFocusMode.Player);
        }
    }
}