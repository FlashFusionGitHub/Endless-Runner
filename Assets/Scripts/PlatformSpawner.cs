using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // The platform to spawn
    [SerializeField]
    Platform platform;
    // The frequecny in which platforms are spawned
    [SerializeField]
    float spawnTime;

    // Spawn Timer - used to count down the the spawnTime
    float spawnTimer;

    // The min / max Y values at which the platform can spawn at; 
    [SerializeField]
    float minYSpawn, maxYSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            Instantiate(platform, new Vector2(transform.position.x, Random.Range(minYSpawn, maxYSpawn)), Quaternion.identity);
            spawnTimer = spawnTime;
        }
    }
}
