using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An infinitely scrolling walkway
public class Walkway : MonoBehaviour
{
    [SerializeField]
    GameObject[] walkwayTiles;

    [SerializeField]
    int walkwaySpeed;

    bool stopWalkway;

    public bool StopWalkway { get { return stopWalkway; } set { stopWalkway = value; } }

    Vector2[] originPositions = new Vector2[6];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < walkwayTiles.Length; i++)
        {
            originPositions[i] = walkwayTiles[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    void RecycleWalkwayBlock(GameObject go)
    {
        if (go.transform.position.x <= -11.77f)
        {
            go.transform.position = new Vector2(15.23f, go.transform.position.y);
        }
    }

    void DisableWalkway(GameObject go)
    {
        if (go.transform.position.x <= -12f)
        {
            go.SetActive(false);
        }
    }
}
