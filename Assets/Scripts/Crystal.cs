using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PickUp
{
    // The crystals worth to the player
    [SerializeField]
    int m_score;

    private void Start()
    {
        Score = m_score;
    }
}
