using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Player controller can perform the following functions
    - Moving & Jumping
    - Interacting with obstacles
    - Collecting items */

public class PlayerController : MonoBehaviour
{
    // Players max horizontal movement speed
    public int moveSpeed;
    // Players max vertical jumping force
    public int jumpForce;
    // The players current grounded state - is the player touching a platform
    public bool isGrounded;
    // Reference to the players rigidbody component
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get The rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        Jump();
    }

    void MoveHorizontal()
    {
        // store the current horizontal user input 
        float moveInput = Input.GetAxisRaw("Horizontal");
        // Move left or right by applying a horizontal velocity to the player 
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Rotate player transform to face its move direction
        if (moveInput < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        if (moveInput > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);

    }

    void Jump()
    {
        // Apply an Upward force to the player if the UpArrow is pressed and the player is touching a platform
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }

    // Check when the player enters or exits and platform - update grounded state accordingly
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform") 
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
            isGrounded = false;
    }
}
