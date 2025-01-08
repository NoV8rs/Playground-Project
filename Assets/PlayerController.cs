using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject playerPrefab;
    
    [Header("Movement")]
    [SerializeField] [Range(1,10)] private float moveSpeed = 5f;
    [SerializeField] [Range(1,10)] private float mouseSensitivity = 1f;
    [SerializeField] private float jumpForce = 5f;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    
    [Header("Ground Check")]
    //[SerializeField] private Transform groundCheck;
    
    [Header("Jump")]
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPrefab = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
    
    private void Update()
    {
        Rotate();
    }
    
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed).normalized;
    }
    
    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerPrefab.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        yRotation += mouseX;
        playerPrefab.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
