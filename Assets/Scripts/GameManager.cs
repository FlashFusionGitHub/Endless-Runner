using System.Collections;
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

    bool doOnce;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard.gameObject.SetActive(false);
        AudioManager.Instance.PlayAudio("Music");
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddScore());


        if(player.Died)
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
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void End()
    {
        retryButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

        AudioManager.Instance.GetAudio("Music").source.Stop();

        if (player.Score > scoreboard.HighestScore)
        {
            AudioManager.Instance.PlayAudio("Win");
            NewHighscoreText.gameObject.SetActive(true);
        }
        else
        {
            AudioManager.Instance.PlayAudio("Lose");
            diedText.gameObject.SetActive(true);
        }


        scoreboard.AddHighScoreEntry(player.Score, "ABC");
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
