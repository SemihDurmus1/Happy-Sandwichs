using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCenter : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsJumping { get; private set; }

    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private void Awake()
    {
        var playerActionMap = inputActions.FindActionMap("Player");

        moveAction = playerActionMap.FindAction("Move");
        lookAction = playerActionMap.FindAction("Look");
        jumpAction = playerActionMap.FindAction("Jump");

        moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => MoveInput = Vector2.zero;

        lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => LookInput = Vector2.zero;

        jumpAction.performed += ctx => IsJumping = true;
        jumpAction.canceled += ctx => IsJumping = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
    }
}
