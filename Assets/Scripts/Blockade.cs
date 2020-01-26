using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    float blockadeSpeed;

    public float BlockadeSpeed { get { return blockadeSpeed; } set { blockadeSpeed = value; } }

    float obstaclePosition;

    public GameObject[] blocks;

    public int numberOfBlocksToEnable;

    public int NumberOfBlocksToEnable { get { return numberOfBlocksToEnable; } set { numberOfBlocksToEnable = value; } }

    int numberOfBlocksEnabled;
    private void OnEnable()
    {
        obstaclePosition = transform.position.x;

        EnableBlocks();
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

    //Randomly enable blocks
    void EnableBlocks()
    {
        for (int i = 0; i < numberOfBlocksToEnable; i++)
        {
            int rnd = Random.Range(0, blocks.Length);

            if (!blocks[rnd].activeSelf)
                blocks[rnd].SetActive(true);

            else i--;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if(blocks[i].activeSelf)
                blocks[i].SetActive(false);
        }

        numberOfBlocksEnabled = 0;
    }
}
