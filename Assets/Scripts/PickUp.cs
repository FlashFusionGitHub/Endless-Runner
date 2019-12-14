using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Used for selecting a crystals size
    public enum CrystalSizes { SMALL = 1, MEDIUM, LARGE };

#pragma warning disable 649
    // The crystals size
    [SerializeField]
    CrystalSizes crystalSize;
    // The crystals worth to the player
    [SerializeField]
    int score;
    // The amount of fuel the crystal while provide
    [SerializeField]
    float fuel;
#pragma warning restore 649

    // Getters and setters for various pickup attributes
    public CrystalSizes CrystalSize { get { return crystalSize; } set { crystalSize = value; } }
    public int Score { get { return score; } set { score = value; } }
    public float Fuel { get { return fuel; } set { fuel = value; } }
}
