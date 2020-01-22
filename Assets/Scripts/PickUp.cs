using UnityEngine;

public class PickUp : MonoBehaviour
{
    // The crystals worth to the player
    int score;
    // The amount of fuel the Potion while provide
    float fuel;

    // Getters and setters for various pickup attributes
    public int Score { get { return score; } set { score = value; } }
    public float Fuel { get { return fuel; } set { fuel = value; } }
}
