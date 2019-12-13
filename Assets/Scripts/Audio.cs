using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Audio class is used for holding every important audio attribute in one place
[System.Serializable]
public class Audio
{
    // Used for selecting audio type ( is this audio clip music or sfx)
    public enum AudioType { MUSIC = 1, SFX };

    // The clips name
    [SerializeField]
    string audioName;

    // The audio clip
    [SerializeField]
    AudioClip clip;

    [SerializeField]
    AudioType audioType;

    // The audio clips volume - ( min = 0 max = 1 )
    [SerializeField]
    [Range(0, 1)]
    float volume;

    // Enable looping audio
    [SerializeField]
    bool loop;

    // The Audio source component
    [HideInInspector]
    public AudioSource source;

    // Getters and Setters for Audio attributes
    public string AudioName { get { return audioName; } }
    public AudioClip Clip { get { return clip; } }
    public AudioType _AudioType { get { return audioType; } }
    public float Volume { get { return volume; } set { volume = value; } }
    public bool Loop { get { return loop; } }
}