using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
#pragma warning disable 649
    // reference to the gameobject the platform activates
    [SerializeField]
    GameObject spikes, fuel, crystal;
    // reference the position the objects spawn at
    [SerializeField]
    List<Transform> objectSpawnPositions;
#pragma warning restore 649

    // the platforms max speed
    float platformSpeed;
    // The obstacles orginal position
    float obstaclePosition;

    //Getter and Setter for the platform speed
    public float PlatformSpeed { get { return platformSpeed; } set { platformSpeed = value; } }

    private void OnEnable()
    {
        foreach (Transform t in objectSpawnPositions)
        {
            RNGSpawner(t);
        }
    }

    void Start()
    {
        obstaclePosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform each frame
        transform.position = new Vector2(obstaclePosition -= platformSpeed * Time.deltaTime, transform.position.y);

        if(gameObject.transform.position.x < -17f)
        {
            obstaclePosition = 17f;

            gameObject.SetActive(false);
        }
    }


    bool spawnedSpikes, spawnedPotion, spawnedCrystal;
    // spawn a crystal, potion or obstacles, ensuring every platform has at least one of each
    void RNGSpawner(Transform t)
    {
        // A random number to represent the object to spawn (0 = spikes, 1 = potion,  2 = crystal)
        int randomNumber = Random.Range(0, 3);

        if(randomNumber == 0 && spawnedSpikes == false)
        {
            spawnedSpikes = true;

            spikes.transform.position = new Vector2(t.position.x, t.position.y + 0.5f);

            spikes.SetActive(true);
        }
        else if (randomNumber == 1 && spawnedPotion == false)
        {
            spawnedPotion = true;

            fuel.transform.position = new Vector2(t.position.x, t.position.y + 0.45f);

            fuel.SetActive(true);
        }
        else if (randomNumber == 2 && spawnedCrystal == false)
        {
            spawnedCrystal = true;

            crystal.transform.position = new Vector2(t.position.x, t.position.y + 1);

            crystal.SetActive(true);
        }
        else
        {
            RNGSpawner(t);
        }
    }

    // disable all activate objects
    private void OnDisable()
    {
        spawnedSpikes = false;
        spawnedPotion = false;
        spawnedCrystal = false;
    }
}
