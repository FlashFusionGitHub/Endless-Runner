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
    // A pool of platforms
    [SerializeField]
    List<Platform> pooledPlatforms;
    // A pool of blockades
    [SerializeField]
    List<Blockade> pooledBlockades;
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
    float spawnTimer;

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
        spawnTimer = spawnTime;
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
                    Platform platform = retreivePooledPlatform();

                    if (platform == null)
                    {
                        AddObjectsToPool(pooledPlatforms[0].gameObject);
                        platform = retreivePooledPlatform();
                    }

                    platform.transform.position = new Vector2(17f, Random.Range(minYSpawn, maxYSpawn));

                    platform.transform.rotation = Quaternion.identity;

                    platform.PlatformSpeed = obstacleSpeed;

                    platform.gameObject.SetActive(true);

                    spawnSwitch = true;
                }
                else
                {
                    Blockade blockade = retreivePooledBlockade();

                    if (blockade == null)
                    {
                        AddObjectsToPool(pooledBlockades[0].gameObject);
                        blockade = retreivePooledBlockade();
                    }

                    blockade.transform.position = new Vector2(17f, 0);

                    blockade.transform.rotation = Quaternion.identity;

                    blockade.NumberOfBlocksToEnable = numBlocks;

                    blockade.BlockadeSpeed = obstacleSpeed;

                    blockade.gameObject.SetActive(true);

                    spawnSwitch = false;
                }

                spawnTimer = spawnTime;
            }
        }
    }

    // Retreives a platform from the pool
    Platform retreivePooledPlatform()
    {
        for (int i = 0; i < pooledPlatforms.Count; i++)
        {
            if (!pooledPlatforms[i].gameObject.activeInHierarchy)
            {
                return pooledPlatforms[i];
            }
        }

        return null;
    }

    // Retreives a platform from the pool
    Blockade retreivePooledBlockade()
    {
        for (int i = 0; i < pooledBlockades.Count; i++)
        {
            if (!pooledBlockades[i].gameObject.activeInHierarchy)
            {
                return pooledBlockades[i];
            }
        }

        return null;
    }

    //Add more objects to the pool
    void AddObjectsToPool(GameObject go)
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject tempGo = Instantiate(go);

            tempGo.gameObject.SetActive(false);

            if (tempGo.GetComponent<Platform>())
            {
                pooledPlatforms.Add(tempGo.GetComponent<Platform>());
            }
            else
            {
                pooledBlockades.Add(tempGo.GetComponent<Blockade>());
            }
        }
    }

    public void IncreaseSpeedOfAllObstacles(float speedIncrease)
    {
        obstacleSpeed += speedIncrease;

        foreach(Platform p in pooledPlatforms)
        {
            p.PlatformSpeed = ObstacleSpeed;
        }

        foreach (Blockade b in pooledBlockades)
        {
            b.BlockadeSpeed = ObstacleSpeed;
        }
    }
}
