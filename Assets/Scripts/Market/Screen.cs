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
        [SerializeField] private Transform screenCamPoint;
        [SerializeField] private float camSmoothness;

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

            GameManager.Instance.cameraController.SetCamFocus(CameraFocusMode.Screen);

            playerManager.inputCenter.SetInputEnabled(false); // disable inputs

        }

        public void DeInteract()
        {
            GameManager.Instance.cursorManager.ToggleCursor(false);

            screenCollider.enabled = true;

            GameManager.Instance.cameraController.SetCamFocus(CameraFocusMode.Player);

            playerManager.inputCenter.SetInputEnabled(true); // enable inputs
        }
    }
}