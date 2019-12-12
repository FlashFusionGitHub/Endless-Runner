using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    float currentScore;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddScore());

        if(player.Died)
        {
            End();
        }
    }

    void Idle()
    {
        retryButton.gameObject.SetActive(false);
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
