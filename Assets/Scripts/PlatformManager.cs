using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages every instatiated platform in the scene - used for:
// controlling platforms speed
// instantiating platforms at a given frequency
// destroying platforms

public class PlatformManager : MonoBehaviour
{
    // The platform to spawn
    [SerializeField]
    Platform platform;

    // The frequency in which platforms are spawned
    [SerializeField]
    float spawnTime;

    // Spawn Timer - used to count down the the spawnTime
    float spawnTimer;

    // The min / max Y values at which the platform can spawn at; 
    [SerializeField]
    float minYSpawn, maxYSpawn;

    // the platforms max move speed
    [SerializeField]
    int platformSpeed;

    // A list to hold all instantiated platforms
    List<Platform> platforms = new List<Platform>();

    // boolean used to toogle when a new platform is instantiated
    bool spawnPlatforms;

    // Getter and Setter for spawnPlatforms boolean
    public bool SpawnPlatform { get { return spawnPlatforms; } set { spawnPlatforms = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnPlatforms)
        {
            if (spawnTimer <= 0)
            {
                Platform plat = Instantiate(platform, new Vector2(transform.position.x, Random.Range(minYSpawn, maxYSpawn)), Quaternion.identity);

                plat.PlatformSpeed = platformSpeed;

                platforms.Add(plat);

                spawnTimer = spawnTime;
            }
        }

        DestoryPlatform();
    }

    // Destroys platforms once they reach a specific distance
    void DestoryPlatform()
    {
        foreach(Platform plat in platforms.ToArray())
        {
            if(plat.transform.position.x <= -12)
            {
                Platform tempPlat = plat;

                platforms.Remove(plat);

                DestroyImmediate(tempPlat.gameObject);
            }
        }
    }
}
