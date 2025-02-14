using Ingredient;
using Player.Input;
using Sandwich;
using UnityEngine;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Grabbing,
        Placing
    }
    public class PlayerManager : MonoBehaviour
    {
        public PlayerInputCenter inputCenter;

        public Transform camTransform;
        public Transform grabPoint;

        public float pickUpDistance = 2.5f;
        public LayerMask pickUpLayers;
        public LayerMask sandwichPlaneLayer;
        public LayerMask NPCLayer;

        public SandwichMakerPlane currentSandwichPlane;//The sandwich maker plane that we raycasting
        public GrabbableObjectBase currentGrabbable; // Unified variable for both IngredientItem and ResultSandwich

        public PlayerState playerState = PlayerState.Idle;
    }
}