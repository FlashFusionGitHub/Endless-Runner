using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{

    float blockadeSpeed;

    public float BlockadeSpeed { get { return blockadeSpeed; } set { blockadeSpeed = value; } }

    float obstaclePosition;

    public GameObject[] blocks;

    void Start()
    {
        obstaclePosition = transform.position.x;
    }

    private void OnEnable()
    {
        DisableBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform each frame
        transform.position = new Vector2(obstaclePosition -= blockadeSpeed * Time.deltaTime, transform.position.y);

        if (gameObject.transform.position.x < -15)
        {
            obstaclePosition = 17f;

            gameObject.SetActive(false);
        }
    }


    void DisableBlocks() 
    {
        foreach(GameObject go in blocks)
        {
            int num = Random.Range(0, 2);

            if(num == 0)
            {
                go.SetActive(false);
            }
        }
    }

    void EnableBlocks()
    {
        foreach (GameObject go in blocks)
        {
            go.SetActive(true);
        }
    }

    private void OnDisable()
    {
        EnableBlocks();
    }
}
