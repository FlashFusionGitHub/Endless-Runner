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
            RNGSpawner();

            /*if (spawnCrystal)
            {
                Instantiate(pickUps[Random.Range(0, pickUps.Count)], new Vector2(t.position.x, t.position.y + 1), Quaternion.identity, transform);
            }
            else
            {
                Instantiate(obstacle, new Vector2(t.position.x, t.position.y + 0.5f), Quaternion.identity, transform);
            }*/
        }

        obstaclePosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform each frame
        transform.position = new Vector2(obstaclePosition -= platformSpeed * Time.deltaTime, transform.position.y);
    }

    // Used to track the number of obstacles and crystals spawned on a platform
    int obstacleCount, crystalCount;
    // randomly spawn crystals and obstacles, ensuring every platform has at least one obstacle or one crystal
    void RNGSpawner()
    {
        // A random number to represent the object to spawn (0 = obstacle, 1 = crystal)
        int randomNumber = Random.Range(0, 2);

        if(randomNumber == 0)
        {
            obstacleCount += 1;

            if (obstacleCount < 2)
                spawnCrystal = false;
            else
                spawnCrystal = true;
        }
        else if (randomNumber == 1)
        {
            crystalCount += 1;

            if (crystalCount < 2)
                spawnCrystal = true;
            else
                spawnCrystal = false;
        }
    }
}
