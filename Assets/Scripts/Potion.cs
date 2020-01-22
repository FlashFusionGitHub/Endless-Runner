using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : PickUp
{
    // The amount of fuel the Potion while provide
    [SerializeField]
    int m_fuel;

    private void Start()
    {
        Fuel = m_fuel;
    }
}
