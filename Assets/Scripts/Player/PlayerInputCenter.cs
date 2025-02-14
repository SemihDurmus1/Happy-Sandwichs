using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputCenter : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsJumping { get; private set; }

        [Header("Grabbing")]
        public bool IsGrabKeyPressed { get; private set; }
        public bool IsGrabKeyHolding { get; set; }

        [Header("Interact")]
        public bool IsInteractKeyPressed { get; private set; }
        public bool IsInteractKeyHolding { get; set; }

        [SerializeField] private InputActionAsset inputActions;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction grabAction;
        private InputAction interactAction;

        private void Awake()
        {
            var playerActionMap = inputActions.FindActionMap("Player");

            moveAction = playerActionMap.FindAction("Move");
            lookAction = playerActionMap.FindAction("Look");
            jumpAction = playerActionMap.FindAction("Jump");
            grabAction = playerActionMap.FindAction("Grab");
            interactAction = playerActionMap.FindAction("Interact");

            moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += ctx => MoveInput = Vector2.zero;

            lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            lookAction.canceled += ctx => LookInput = Vector2.zero;

            jumpAction.performed += ctx => IsJumping = true;
            jumpAction.canceled += ctx => IsJumping = false;

            grabAction.performed += ctx => IsGrabKeyPressed = true;
            grabAction.canceled += ctx => IsGrabKeyPressed = false;

            interactAction.performed += ctx => IsInteractKeyPressed = true;
            interactAction.canceled += ctx => IsInteractKeyPressed = false;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            grabAction.Enable();
            interactAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            grabAction.Disable();
            interactAction.Disable();
        }
    }
}