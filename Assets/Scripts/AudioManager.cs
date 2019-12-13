using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Manages all audio available in the game
public class AudioManager : MonoBehaviour
{
    // Array of audio objects 
    [SerializeField]
    Audio[] myAudio;

    [SerializeField]
    Slider sfxSlider, musicSlider;

    // Set the instance to this AudioManager if it is null on awake
    void Awake()
    {
        gameObject.SetActive(true);

        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        }

        if(!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialise AudioSource component attributes on start
        for (int i = 0; i < myAudio.Length; i++)
        {
            myAudio[i].source = gameObject.AddComponent<AudioSource>();
            myAudio[i].source.clip = myAudio[i].Clip;

            if(myAudio[i]._AudioType == Audio.AudioType.SFX)
                myAudio[i].source.volume = PlayerPrefs.GetFloat("SFXVolume");

            if (myAudio[i]._AudioType == Audio.AudioType.MUSIC)
                myAudio[i].source.volume = PlayerPrefs.GetFloat("MusicVolume");

            myAudio[i].source.loop = myAudio[i].Loop;
        }

        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Update()
    {

    }

    // Plays audio with a given name
    public void PlayAudio(string audioName)
    {
        for(int i = 0; i < myAudio.Length; i++)
        {
            if(audioName == myAudio[i].AudioName)
            {
                myAudio[i].source.Play();
            }
        }
    }

    // Returns Audio with a given name
    public Audio GetAudio(string audioName)
    {
        for (int i = 0; i < myAudio.Length; i++)
        {
            if (audioName == myAudio[i].AudioName)
            {
                return myAudio[i];
            }
        }

        Debug.LogWarning("Could not find " + audioName);

        return null;
    }

    public void AdjustMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);

        foreach (Audio audio in myAudio)
        {
            if (audio._AudioType == Audio.AudioType.MUSIC)
                audio.source.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public void AdjustSFXVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SFXVolume", slider.value);

        foreach (Audio audio in myAudio)
        {
            if (audio._AudioType == Audio.AudioType.SFX)
                audio.source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    // Mutes all audio in the audio array
    public void MuteAudio()
    {
        for (int i = 0; i < myAudio.Length; i++)
        {
            myAudio[i].source.volume = 0;
        }
    }

    // UnMutes all audio in the audio array
    public void UnMuteAudio()
    {
        for (int i = 0; i < myAudio.Length; i++)
        {
            if(myAudio[i]._AudioType == Audio.AudioType.MUSIC)
                myAudio[i].source.volume = PlayerPrefs.GetFloat("MusicVolume");
            if (myAudio[i]._AudioType == Audio.AudioType.SFX)
                myAudio[i].source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }
}
