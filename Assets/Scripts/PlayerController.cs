using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Player controller can perform the following functions
    - Moving & Jumping
    - Interacting with obstacles
    - Collecting items */

public class PlayerController : MonoBehaviour
{
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
    // Reference to the players animator
    Animator anim;

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

    bool boost;

    // Getters for score and died
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
    public void PlayerUpdate()
    {
        UpdateFuelGauge();

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

        if(boost)
        {
            Propulsion();
        }
        else
        {
            if (audioManager.GetAudio("JetPack").source.isPlaying)
                audioManager.GetAudio("JetPack").source.Stop();
        }

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

    void Propulsion()
    {
        // Apply an Upward force to the player
        if (currentFuel > 0f)
        {
            currentFuel -= 0.1f;

            rb.AddForce(new Vector2(0, propulsionForce), ForceMode2D.Force);

            if (!audioManager.GetAudio("JetPack").source.isPlaying)
                audioManager.PlayAudio("JetPack");
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
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
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
            if (audioManager.GetAudio("JetPack").source.isPlaying)
                audioManager.GetAudio("JetPack").source.Stop();

            audioManager.PlayAudio("Die");

            died = true;
            gameObject.SetActive(false);
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

    public void boostOnPress()
    {
        boost = true;
    }

    public void boostOnRelease()
    {
        boost = false;
    }
}
