using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private bool isGrounded;
    public float speed;
    public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        MouseMovement();
        PlayerJump();
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        _playerRb.AddForce(move * speed);
    }
    
    private void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.right, mouseY);
    }
    
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
