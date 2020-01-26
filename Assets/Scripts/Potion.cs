using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : PickUp
{
#pragma warning disable 649
    // The amount of fuel the Potion while provide
    [SerializeField]
    int m_fuel;
#pragma warning restore 649

    private void Start()
    {
        Fuel = m_fuel;
    }
}
