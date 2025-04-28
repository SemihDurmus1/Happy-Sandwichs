using UnityEngine;

namespace Player.Camera
{
    public enum CameraFocusMode
    {
        Player,
        Screen
    }
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform camTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform screenTransform;

        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private CameraFocusMode camFocus = CameraFocusMode.Player;

        private void FixedUpdate()
        {
            switch (camFocus)
            {
                case CameraFocusMode.Player:
                    MoveCamera(playerTransform);
                    break;
                case CameraFocusMode.Screen:
                    MoveCamera(screenTransform);
                    camTransform.rotation = screenTransform.rotation;
                    break;
            }
        }

        public void SetCamFocus(CameraFocusMode focusMode)
        {
            camFocus = focusMode;
        }
        
        public void MoveCamera(Transform targetTransform)
        {
            Vector3 targetPosition = Vector3.Lerp(camTransform.position, targetTransform.position, Time.fixedDeltaTime * smoothSpeed);
            //Quaternion targetRotation = Quaternion.Lerp(camTransform.rotation, targetTransform.rotation, Time.fixedDeltaTime * smoothSpeed);
            camTransform.position = targetPosition;
            //camTransform.rotation = targetRotation;
        }
    }
}