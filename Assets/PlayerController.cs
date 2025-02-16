using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlaygroundProject _inputActions;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.84f;
    private bool doubleJump = false; // If double jump can activate
    public bool canDoubleJump = false; // Double Jump is enabled
    
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        // Store original camera position
        originalCameraPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HeadBobbing();
    }
    
    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlaygroundProject();
            _inputActions.Player.Enable();
        }
        _inputActions.Player.Jump.performed += Jump;
    }
    
    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump;
        _inputActions.Player.Disable();
    }
    
    void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            doubleJump = false;
        }
        else if (canDoubleJump && !doubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            doubleJump = true;
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset gravity when grounded
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    
    void HeadBobbing() // Can be optimized
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


