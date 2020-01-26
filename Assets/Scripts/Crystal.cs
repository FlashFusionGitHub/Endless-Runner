using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PickUp
{
#pragma warning disable 649
    // The crystals worth to the player
    [SerializeField]
    int m_score;
#pragma warning restore 649

    private void Start()
    {
        Score = m_score;
    }
}
