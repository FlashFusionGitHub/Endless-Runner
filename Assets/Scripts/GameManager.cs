﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manages the Game states
public class GameManager : MonoBehaviour
{
    // References to various buttons
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button retryButton;
    [SerializeField]
    Button pauseButton, resumeButton;

    // Reference to the platform manager
    [SerializeField]
    PlatformManager platformManager;

    // Reference to the walkway
    [SerializeField]
    Walkway walkway;

    // Reference to the Died text UI element
    [SerializeField]
    Text diedText;

    // Reference to the new highscore text UI element
    [SerializeField]
    Text NewHighscoreText;

    // Reference to the score text UI element
    [SerializeField]
    Text scoreText;

    // Reference to the player
    [SerializeField]
    PlayerController player;

    // Temp current score
    float currentScore;

    // Reference to the scoreboard
    [SerializeField]
    Scoreboard scoreboard;

    // Reference to the fuel gauge UI element
    [SerializeField]
    GameObject fuelGauge;

    bool doOnce;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    Button jumpButton, boostButton;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard.gameObject.SetActive(false);
        audioManager.PlayAudio("Music");
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddScore());

        if (player.Died)
        {
            if (!doOnce)
            {
                End();

                doOnce = true;
            }
        }
    }

    void Idle()
    {
        platformManager.SpawnPlatform = false;
        walkway.gameObject.SetActive(true);
    }

    public void Play()
    {
        startButton.gameObject.SetActive(false);
        walkway.StopWalkway = true;
        platformManager.SpawnPlatform = true;

#if UNITY_ANDROID && !UNITY_EDITOR
        jumpButton.gameObject.SetActive(true);
        boostButton.gameObject.SetActive(true);
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void End()
    {
        fuelGauge.SetActive(false);
        retryButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

        audioManager.GetAudio("Music").source.Stop();

        if (player.Score > scoreboard.HighestScore)
        {
            audioManager.PlayAudio("Win");
            NewHighscoreText.gameObject.SetActive(true);
        }
        else
        {
            audioManager.PlayAudio("Lose");
            diedText.gameObject.SetActive(true);
        }

        scoreboard.AddHighScoreEntry(player.Score);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    IEnumerator AddScore()
    {
        while (currentScore < player.Score)
        {
            currentScore += Time.deltaTime;
            currentScore = Mathf.Clamp(currentScore, 0f, player.Score);
            scoreText.text = Mathf.RoundToInt(currentScore).ToString();
            yield return null;
        }
    }
}
