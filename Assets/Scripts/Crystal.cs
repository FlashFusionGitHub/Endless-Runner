using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PickUp
{
    // The crystals worth to the player
    [SerializeField]
    int m_score;

    public Crystal()
    {
        Score = m_score;
    }
}
