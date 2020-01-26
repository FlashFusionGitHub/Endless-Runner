using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{

#pragma warning disable 649
    //Reference to the blocks to activate
    [SerializeField]
    GameObject[] blocks;
#pragma warning restore 649

    float blockadeSpeed;

    float obstaclePosition;

    int numberOfBlocksToEnable;

    public float BlockadeSpeed { get { return blockadeSpeed; } set { blockadeSpeed = value; } }

    public int NumberOfBlocksToEnable { get { return numberOfBlocksToEnable; } set { numberOfBlocksToEnable = value; } }

    private void OnEnable()
    {
        obstaclePosition = transform.position.x;

        EnableBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the blockade each frame
        transform.position = new Vector2(obstaclePosition -= blockadeSpeed * Time.deltaTime, transform.position.y);

        if (gameObject.transform.position.x < -17f)
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
            int randNum = Random.Range(0, blocks.Length);

            if (!blocks[randNum].activeSelf)
                blocks[randNum].SetActive(true);

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
    }
}
