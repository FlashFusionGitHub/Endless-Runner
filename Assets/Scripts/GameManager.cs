using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Idle Game
    // Play Game
    // End Game 
    // Pause / Resume
    // Mute / Unmute audio

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button retryButton;

    [SerializeField]
    Button pauseButton, resumeButton;

    [SerializeField]
    PlatformManager platformManager;

    [SerializeField]
    Walkway walkway;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = player.Score.ToString();

        if(player.Died)
        {
            End();
        }
    }

    void Idle()
    {
        retryButton.gameObject.SetActive(false);

        platformManager.gameObject.SetActive(false);
        walkway.gameObject.SetActive(true);
    }

    public void Play()
    {
        startButton.gameObject.SetActive(false);
        walkway.StopWalkway = true;

        platformManager.gameObject.SetActive(true);
    }

    void End()
    {
        retryButton.gameObject.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ToogleMute()
    {

    }
}
