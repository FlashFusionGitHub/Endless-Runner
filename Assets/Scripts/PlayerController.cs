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
    [SerializeField]
    int moveSpeed;
    // Players vertical propulsion force - for jetpack
    [SerializeField]
    int propulsionForce;
    // Players max vertical jumping force
    [SerializeField]
    int jumpForce;
    // The players current grounded state - is the player touching a platform
    [SerializeField]
    bool isGrounded;
    // Reference to the players rigidbody component
    Rigidbody2D rb;
    //Reference to the players animator
    Animator anim;
    // Jetpack current fuel amount
    [SerializeField]
    float fuel;
    // The players current score
    [SerializeField]
    int score;
    // The players dead / alive state
    [SerializeField]
    bool died;

    public int Score { get { return score; } }

    public bool Died { get { return died; } }

    // Start is called before the first frame update
    void Start()
    {
        // Get the players rigidbody component
        rb = GetComponent<Rigidbody2D>();
        // Get the players animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Propulsion();

        if(isGrounded)
            Jump();

        if (fuel > 0f)
        {
            // sets isFalling to false if the player collects fuel while falling
            anim.SetBool("isFalling", false);

            // check player is touching ground - set isJumping animator state accordingly
            if (isGrounded)
            {
                anim.SetBool("isJumping", false);
            }
            else if (!isGrounded)
            {
                anim.SetBool("isJumping", true);
            }
        }
        else
        {
            // sets isJumping to false if the player runs out of fuel mid flight
            anim.SetBool("isJumping", false);

            // check player is touching ground - set isJumping animator state accordingly
            if (isGrounded)
            {
                anim.SetBool("isFalling", false);
            }
            else if (!isGrounded)
            {
                anim.SetBool("isFalling", true);
            }
        }
    }

    void Propulsion()
    {
        // Apply an Upward force to the player while the UpArrow is pressed and the player is touching a platform
        if (Input.GetKey(KeyCode.UpArrow) && fuel > 0f)
        {
            fuel -= 0.1f;

            rb.AddForce(new Vector2(0, propulsionForce), ForceMode2D.Force);
        }
    }

    void Jump()
    {
        // Apply an Upward force to the player if the UpArrow is pressed and the player is touching a platform
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
    }

    void Collect(PickUp pickUp)
    {
        fuel += pickUp.Fuel;
        score += pickUp.Score;
    }

    // Check when the player enters or exits and platform - update grounded state accordingly
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGrounded)
        {
            if (collision.collider.tag == "Platform")
            {
                isGrounded = true;
            }
        }

        if (collision.collider.tag == "Spikes")
        {
            died = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickUp")
        {
            Collect(collision.GetComponent<PickUp>());
            Destroy(collision.gameObject);
        }
    }
}
