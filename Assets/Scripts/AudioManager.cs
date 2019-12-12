using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Manages all audio available in the game
public class AudioManager : MonoBehaviour
{
    // Array of audio objects 
    [SerializeField]
    Audio[] myAudio;

    // Singleton instance of the AudioManager
    static AudioManager instance;
    // Getter for the AudioManager singleton instance
    public static AudioManager Instance { get { return instance; } }

    // Set the instance to this AudioManager if it is null on awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialise AudioSource component attributes on start
        for(int i = 0; i < myAudio.Length; i++)
        {
            myAudio[i].source = gameObject.AddComponent<AudioSource>();
            myAudio[i].source.clip = myAudio[i].Clip;
            myAudio[i].source.volume = myAudio[i].Volume;
            myAudio[i].source.loop = myAudio[i].Loop;
        }
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
            myAudio[i].source.volume = 1;
        }
    }
}
