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
    PlatformSpawner platformSpawner;

    [SerializeField]
    Walkway walkway;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Idle()
    {
        retryButton.gameObject.SetActive(false);

        platformSpawner.gameObject.SetActive(false);
        walkway.gameObject.SetActive(true);
    }

    public void Play()
    {
        startButton.gameObject.SetActive(false);
        walkway.StopWalkway = true;

        platformSpawner.gameObject.SetActive(true);
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
