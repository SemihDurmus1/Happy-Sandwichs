using UnityEngine;

namespace Player.Movement
{
    public class GroundChecker : MonoBehaviour
    {
        [Header("Ground Check Settings")]
        [SerializeField] private LayerMask groundLayers; // Array for multiple ground layers
        [SerializeField] private float groundCheckRadius; // Radius of the sphere check
        [SerializeField] private Transform groundCheckPoint; // Point for checking ground
        public bool CanJump { get; set; } = false; // To prevent continuous jumping while holding the button

        public bool IsGrounded()
        {
            return Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayers);
        }

        private void OnDrawGizmos()
        {
            if (groundCheckPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(groundCheckPoint.position, groundCheckRadius);
            }
        }
    }
}