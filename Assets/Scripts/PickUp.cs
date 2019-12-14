using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Used for selecting a crystals size
    public enum CrystalSizes { SMALL = 1, MEDIUM, LARGE };

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
}
