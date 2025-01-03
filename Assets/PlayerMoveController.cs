using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private PlayerInputCenter inputCenter;
    [SerializeField] private Transform cameraTransform; // For cam rotation

    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 100f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(
            inputCenter.LookInput.y * lookSensitivity * Time.deltaTime * Vector3.left +
            inputCenter.LookInput.x * lookSensitivity * Time.deltaTime * Vector3.up
            );
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(inputCenter.MoveInput.x, 0, inputCenter.MoveInput.y);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

}
