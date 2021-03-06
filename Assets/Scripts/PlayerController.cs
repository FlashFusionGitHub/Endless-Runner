﻿using UnityEngine;
using UnityEngine.UI;

/* Player controller can perform the following functions
    - Jumping
    - Interacting with obstacles
    - Collecting items */

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649
    // Players vertical propulsion force - for jetpack
    [SerializeField]
    int propulsionForce;
    // Players max vertical jumping force
    [SerializeField]
    int jumpForce;
    // The players current grounded state - is the player touching a platform
    [SerializeField]
    bool isGrounded;
    // Jetpack current fuel amount
    [SerializeField]
    float currentFuel;
    // The max fuel the play can carry
    [SerializeField]
    float maxFuel;
    // The players current score
    [SerializeField]
    int score;
    // The players dead / alive state
    [SerializeField]
    bool died;
    // Reference to audio manager
    [SerializeField]
    AudioManager audioManager;
    // Reference to the fuel gauge image
    [SerializeField]
    Image fuelGauge;
#pragma warning restore 649

    // Reference to the players rigidbody component
    Rigidbody2D rb;
    // Reference to the players animator
    Animator anim;

    //reference to the game manager
    GameManager gameManager;

    bool boost;

    bool enableWindowsControls;

    // Getters for score and died
    public int Score { get { return score; } }
    public bool Died { get { return died; } }

    // setter for disable windows Controls
    public bool EnableWindowsControls { set { enableWindowsControls = value; } }

    // Start is called before the first frame update
    void Start()
    {
        // Get the players rigidbody component
        rb = GetComponent<Rigidbody2D>();
        // Get the players animator component
        anim = GetComponent<Animator>();
        // Get the game manager component
        gameManager = FindObjectOfType<GameManager>();
    }

    // PlayerUpdate is called once per frame
    public void PlayerUpdate()
    {
        if(enableWindowsControls)
            WindowsandUnityControls();

        UpdateFuelGauge();
        JetPack();
        playerAnimations();
    }

    void JetPack()
    {
        if (boost)
        {
            // Apply an Upward force to the player
            if (currentFuel > 0f)
            {
                rb.velocity = new Vector2(0, propulsionForce);

                
                currentFuel -= Time.deltaTime * 5f;

                if (!audioManager.GetAudio("JetPack").source.isPlaying)
                    audioManager.PlayAudio("JetPack");
            }
            else
            {
                if (audioManager.GetAudio("JetPack").source.isPlaying)
                    audioManager.GetAudio("JetPack").source.Stop();
            }
        }
        else
        {
            if (audioManager.GetAudio("JetPack").source.isPlaying)
                audioManager.GetAudio("JetPack").source.Stop();
        }
    }

    public void Jump()
    {
        // Apply an Upward force to the player if player is touching a platform
        if (isGrounded)
        {
            audioManager.PlayAudio("Jump");

            rb.velocity = new Vector2(0, jumpForce);
        }

    }

    void Collect(PickUp pickUp)
    {
        audioManager.PlayAudio("Collect");
        
        currentFuel += pickUp.Fuel;

        score += pickUp.Score;
    }

    // Update fuel gauage image fill amount 
    void UpdateFuelGauge()
    {
        // check that current fuel does not exceed max fuel
        if (currentFuel > maxFuel)
            currentFuel = maxFuel;

        fuelGauge.fillAmount = currentFuel / maxFuel;
    }

    private void WindowsandUnityControls()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR

        if (Input.GetKey(KeyCode.UpArrow))
        {
            boost = true;
        }
        else
        {
            boost = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
#endif
    }

    void playerAnimations()
    {
        if (fuelGauge.fillAmount > 0f)
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

    // Evaluates collision stay between Colliders and this component
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check when the player enters or exits and platform - update grounded state accordingly
        if (!isGrounded)
        {
            if (collision.collider.tag == "Platform")
            {
                isGrounded = true;
            }
        }
    }

    // Evaluates collision enter between Colliders and this component
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Spikes")
        {
            if(audioManager.GetAudio("JetPack").source.isPlaying)
                audioManager.GetAudio("JetPack").source.Stop();

            if (audioManager.GetAudio("Jump").source.isPlaying)
                audioManager.GetAudio("Jump").source.Stop();

            audioManager.PlayAudio("Die");

            died = true;

            gameObject.SetActive(false);
        }
    }

    // Evaluates collision exit between Colliders and this component
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            isGrounded = false;
        }
    }

    // Evaluates Collisions between Trigger Colliders and and this component
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickUp")
        {
            Collect(collision.GetComponent<PickUp>());
            collision.gameObject.SetActive(false);

            if(collision.GetComponent<Crystal>())
                gameManager.IncreaseDifficulty();
        }
    }

    // Used for android UI buttons
    // This function is added to the UI Boost Button 'On Press Event'
    public void boostOnPress()
    {
        boost = true;
    }
    // Used for android UI buttons
    // This function is added to the UI Boost Button 'On Release Event'
    public void boostOnRelease()
    {
        boost = false;

        if (audioManager.GetAudio("JetPack").source.isPlaying)
            audioManager.GetAudio("JetPack").source.Stop();
    }
}
