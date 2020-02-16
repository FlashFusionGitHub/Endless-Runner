using System.Collections.Generic;
using UnityEngine;

// Manages every instatiated platform in the scene - used for:
// controlling platforms speed
// instantiating platforms at a given frequency
// destroying platforms
// Also manages the object pool for the the platforms and blockades

public class PlatformManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    ObjectPooler platformObjectPooler, blockadesObjectPooler;
    // The frequency in which platforms are spawned
    [SerializeField]
    float spawnTime;
    // The min / max Y values at which the platform can spawn at; 
    [SerializeField]
    float minYSpawn, maxYSpawn;
    // the platforms max move speed
    [SerializeField]
    float obstacleSpeed;
#pragma warning restore 649

    // Spawn Timer - used to count down the the spawnTime
    public float spawnTimer;

    // boolean used to toogle when a new platform is instantiated
    bool spawnPlatforms;

    // Getter and Setter for spawnPlatforms boolean
    public bool SpawnPlatform { get { return spawnPlatforms; } set { spawnPlatforms = value; } }
    // Getter and Setter for spawnTime
    public float SpawnTime { get { return spawnTime; } set { spawnTime = value; } }
    // Getter and Setter for obstacleSpeed
    public float ObstacleSpeed { get { return obstacleSpeed; } set { obstacleSpeed = value; } }

    //number of blocks to disable
    public int numBlocks;

    // Start is called before the first frame update
    void Start()
    {
    }

    bool spawnSwitch;
    // Update is called once per frame
    public void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnPlatforms)
        {
            if (spawnTimer <= 0)
            {
                if (!spawnSwitch)
                {
                    GameObject platformGameObject = platformObjectPooler.RetreivePooledObject();

                    platformGameObject.transform.position = new Vector2(17f, Random.Range(minYSpawn, maxYSpawn));

                    platformGameObject.transform.rotation = Quaternion.identity;

                    platformGameObject.GetComponent<Platform>().PlatformSpeed = obstacleSpeed;

                    platformGameObject.gameObject.SetActive(true);

                    spawnSwitch = true;
                }
                else
                {
                    GameObject blockadeGameObject = blockadesObjectPooler.RetreivePooledObject();

                    blockadeGameObject.transform.position = new Vector2(17f, 0);

                    blockadeGameObject.transform.rotation = Quaternion.identity;

                    blockadeGameObject.GetComponent<Blockade>().NumberOfBlocksToEnable = numBlocks;

                    blockadeGameObject.GetComponent<Blockade>().BlockadeSpeed = obstacleSpeed;

                    blockadeGameObject.gameObject.SetActive(true);

                    spawnSwitch = false;
                }

                spawnTimer = spawnTime;
            }
        }
    }

    // increases the speed of all obstacles in the scene
    public void IncreaseSpeedOfAllObstacles(float speedIncrease)
    {
        obstacleSpeed += speedIncrease;

        foreach(GameObject p in platformObjectPooler.objectsToPool)
        {
            p.GetComponent<Platform>().PlatformSpeed = ObstacleSpeed;
        }

        foreach (GameObject b in blockadesObjectPooler.objectsToPool)
        {
            b.GetComponent<Blockade>().BlockadeSpeed = ObstacleSpeed;
        }
    }
}
