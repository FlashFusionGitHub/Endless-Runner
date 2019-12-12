using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Used for selecting a crystals size
    public enum CrystalSizes { small = 1, medium, large };

    // The crystals size
    [SerializeField]
    private CrystalSizes crystalSize;

    // The crystals worth to the player
    [SerializeField]
    private int score;

    // The amount of fuel the crystal while provide
    [SerializeField]
    private float fuel;

    // Getters and setters for various pickup attributes
    public CrystalSizes CrystalSize { get { return crystalSize; } set { crystalSize = value; } }
    public int Score { get { return score; } set { score = value; } }
    public float Fuel { get { return fuel; } set { fuel = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
