using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private PlayerInputCenter inputCenter;
    [SerializeField] private Transform cameraTransform; // For cam rotation

    //These lines can be seperated with settings class or scriptalbeobject
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 100f;

    [Header("Camera Pitch")]
    [SerializeField] private float minCamPitch = -90f;
    [SerializeField] private float maxCamPitch = 90f;
    private float cameraPitch = 0f;//Vertical rotation for camera

    private Rigidbody rb;
    
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
    }

    private void HandleLook()
    {
        transform.Rotate(Vector3.up * inputCenter.LookInput.x * lookSensitivity * Time.deltaTime);

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

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
