using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    GameObject spikes, fuel, crystal;
    [SerializeField]
    List<Transform> objectSpawnPositions;
#pragma warning restore 649

    int platformSpeed;

    public int PlatformSpeed { get { return platformSpeed; } set { platformSpeed = value; } }

    float obstaclePosition;

    bool spawnCrystal;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in objectSpawnPositions)
        {
            RNGSpawner(t);
        }

        obstaclePosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform each frame
        transform.position = new Vector2(obstaclePosition -= platformSpeed * Time.deltaTime, transform.position.y);
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

            Instantiate(spikes, new Vector2(t.position.x, t.position.y + 0.5f), Quaternion.identity, transform);
        }
        else if (randomNumber == 1 && spawnedPotion == false)
        {
            spawnedPotion = true;

            Instantiate(fuel, new Vector2(t.position.x, t.position.y + 0.45f), Quaternion.identity, transform);
        }
        else if (randomNumber == 2 && spawnedCrystal == false)
        {
            spawnedCrystal = true;

            Instantiate(crystal, new Vector2(t.position.x, t.position.y + 1), Quaternion.identity, transform);

        }
        else
        {
            RNGSpawner(t);
        }
    }
}
