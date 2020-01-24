using System.Collections.Generic;
using UnityEngine;

// Manages every instatiated platform in the scene - used for:
// controlling platforms speed
// instantiating platforms at a given frequency
// destroying platforms

public class PlatformManager : MonoBehaviour
{
#pragma warning disable 649
    // A pool of platforms
    [SerializeField]
    List<Platform> pooledPlatforms;
    // A pool of blockades
    [SerializeField]
    List<GameObject> pooledBlockades;
    // The frequency in which platforms are spawned
    [SerializeField]
    float spawnTime;
    // The min / max Y values at which the platform can spawn at; 
    [SerializeField]
    float minYSpawn, maxYSpawn;
    // the platforms max move speed
    [SerializeField]
    int platformSpeed;
#pragma warning restore 649

    // Spawn Timer - used to count down the the spawnTime
    float spawnTimer;

    // boolean used to toogle when a new platform is instantiated
    bool spawnPlatforms;

    // Getter and Setter for spawnPlatforms boolean
    public bool SpawnPlatform { get { return spawnPlatforms; } set { spawnPlatforms = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnPlatforms)
        {
            if (spawnTimer <= 0)
            {
                Platform platform = retreivePooledPlatform();

                platform.transform.position = new Vector2(17f, Random.Range(minYSpawn, maxYSpawn));

                platform.transform.rotation = Quaternion.identity;

                platform.PlatformSpeed = platformSpeed;

                platform.gameObject.SetActive(true);

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
}
