using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlaygroundProject _inputActions;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    private float resetMoveSpeed;
    [Space(20)]
    public float jumpForce = 5f;
    public float gravity = -9.84f;
    [Space(20)]
    private bool doubleJump = false; // If double jump can activate
    public bool canDoubleJump = false; // Double Jump is enabled
    public float doubleJumpForce = 5f; // Force of double jump
    public float doubleJumpGravity = -9.84f; // Gravity of double jump
    
    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    
    // Head Bobbing
    [Header("Head Bobbing Settings")]
    public bool headBobbing = true;
    public float headBobbingSpeed = 10f; // Increased speed for a more natural effect
    public float headBobbingAmount = 0.05f;
    public float returnSpeed = 5f; // Speed of returning to original position
    
    
    
    private Vector3 originalCameraPosition;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        // Store original camera position
        originalCameraPosition = cameraTransform.localPosition;
        resetMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HeadBobbing();
    }
    
    private void OnEnable() // Enable input actions
    {
        if (_inputActions == null)
        {
            _inputActions = new PlaygroundProject(); // Create new input actions
            _inputActions.Player.Enable(); // Enable player input
        }
        _inputActions.Player.Jump.performed += Jump; // Add jump event
    }
    
    private void OnDisable() // Disable input actions
    {
        _inputActions.Player.Jump.performed -= Jump; // Remove jump event
        _inputActions.Player.Disable(); 
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded) // Check if grounded
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Apply jump
            doubleJump = false; // Reset double jump, if enabled
        }
        else if (canDoubleJump && !doubleJump) // Check if double jump is enabled and not already used
        {
            gravity = doubleJumpGravity; // Apply double jump gravity
            velocity.y = Mathf.Sqrt(doubleJumpForce * -2f * gravity); // Apply double jump
            doubleJump = true; // Set double jump to true
        }
    }

    private void HandleMovement() // Player movement
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset gravity when grounded
            gravity = -9.84f; // Reset gravity when grounded
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = resetMoveSpeed;
        }
    }

    private void HandleMouseLook() // Camera rotation
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Get mouse input for X axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Get mouse input for Y axis

        xRotation -= mouseY; // Calculate rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp rotation
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply rotation to camera
        transform.Rotate(Vector3.up * mouseX); // Rotate player
    }

    private void HeadBobbing() // Camera shake when moving
    {
        if (!headBobbing) return; // Skip if head bobbing is disabled

        float horizontal = Input.GetAxis("Horizontal"); // Get input
        float vertical = Input.GetAxis("Vertical"); // Get input
        float speedFactor = new Vector2(horizontal, vertical).magnitude; // Calculate speed factor

        if (speedFactor > 0) // Check if moving
        {
            // Calculate bobbing effect
            float waveSlice = Mathf.Sin(Time.time * headBobbingSpeed); // Calculate wave slice
            float bobAmount = Mathf.Lerp(0, headBobbingAmount, speedFactor); // Scale by movement

            // Apply bobbing to camera
            Vector3 bobbingOffset = new Vector3(0, waveSlice * bobAmount, 0); // Calculate bobbing offset
            cameraTransform.localPosition = originalCameraPosition + bobbingOffset; // Apply bobbing offset
        }
        else
        {
            // Smoothly return to original position when stopping
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalCameraPosition, Time.deltaTime * returnSpeed);
        }
    }
    
    
}


