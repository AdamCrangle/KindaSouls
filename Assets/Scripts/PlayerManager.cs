using UnityEngine;

namespace CosmicJester
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator animator;
        CameraHandler cameraHandler;
        PlayerLocoomotion playerLocoomotion;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerLocoomotion = GetComponent<PlayerLocoomotion>();
            cameraHandler = CameraHandler.singleton;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = animator.GetBool("isInteracting");
            inputHandler.TickInput(delta);
            playerLocoomotion.HandleMovement(delta);
            playerLocoomotion.HandleRollingAndSprinting(delta);
            playerLocoomotion.HandleFalling(delta, playerLocoomotion.moveDirection);

            
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isSprinting = inputHandler.b_Input;

            if (isInAir) 
            {
                playerLocoomotion.inAirTimer = playerLocoomotion.inAirTimer + Time.deltaTime;
            }
        }
    }
}
