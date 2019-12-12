using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An infinitely scrolling walkway
public class Walkway : MonoBehaviour
{
    // Reference to the walkway tile gameobjects
    [SerializeField]
    GameObject[] walkwayTiles;

    // The walkways max move speed
    [SerializeField]
    int walkwaySpeed;

    // Boolean to stop the walkway recycling tiles
    bool stopWalkway;

    // Getter and Setter for stopwalking boolean
    public bool StopWalkway { get { return stopWalkway; } set { stopWalkway = value; } }

    // Update is called once per frame
    void Update()
    {
        // Updates the position of every walkway tile each frame
        foreach(GameObject go in walkwayTiles)
        {
            float position = go.transform.position.x;
            go.transform.position = new Vector2(position -= walkwaySpeed * Time.deltaTime, go.transform.position.y);

            if (!stopWalkway)
            {
                RecycleWalkwayBlock(go);
            }
            else
            {
                DisableWalkway(go);
            }
        }
    }

    // Sets the position of the last tile to the fornt of the queue
    void RecycleWalkwayBlock(GameObject go)
    {
        if (go.transform.position.x <= -11.77f)
        {
            go.transform.position = new Vector2(15.23f, go.transform.position.y);
        }
    }

    // Disables the walkways tiles once they reach a specific X position
    void DisableWalkway(GameObject go)
    {
        if (go.transform.position.x <= -12f)
        {
            go.SetActive(false);
        }
    }
}
