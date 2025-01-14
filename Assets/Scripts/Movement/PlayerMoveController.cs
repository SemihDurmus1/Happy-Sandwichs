using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private PlayerInputCenter inputCenter;
    [SerializeField] private Transform cameraTransform; // For cam rotation
    [SerializeField] private GroundChecker groundChecker;

    [Header("Movement Settings")]//These lines can be seperated with settings class or scriptalbeobject
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 100f;
    [SerializeField] private float jumpForce = 5f;


    [Header("Camera Pitch")]
    [SerializeField] private float minCamPitch = -90f;
    [SerializeField] private float maxCamPitch = 90f;
    private float cameraPitch = 0f;//Vertical rotation for camera

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleLook();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleJump()
    {
        if (inputCenter.IsJumping && groundChecker.IsGrounded() && groundChecker.CanJump)
        {
            Jump();
        }
        else if (!inputCenter.IsJumping && groundChecker.IsGrounded())// Allow jumping again when the button is released and grounded
        {
            groundChecker.CanJump = true;
        }
    }
    private void Jump()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.y = jumpForce;
        rb.linearVelocity = currentVelocity;
        groundChecker.CanJump = false; //Prevents continuous jumping
    }

    private void HandleLook()
    {
        transform.Rotate(inputCenter.LookInput.x * lookSensitivity * Time.deltaTime * Vector3.up);

        cameraPitch -= inputCenter.LookInput.y * lookSensitivity * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, minCamPitch, maxCamPitch);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
    private void HandleMovement()
    {
        //Calculate the move vector according cam direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //reset y axis movement,y moves with character. if we dont reset it, character move at y axis by itself
        forward.y = 0f;
        right.y = 0f;

        //for reduce diagonal movement speed increases
        forward.Normalize();
        right.Normalize();

        //Calculate the move vector
        Vector3 move = forward * inputCenter.MoveInput.y + right * inputCenter.MoveInput.x;

        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * move);
    }
}
