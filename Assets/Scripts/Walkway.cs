﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An infinitely scrolling walkway
public class Walkway : MonoBehaviour
{
    [SerializeField]
    GameObject[] walkwayTiles;

    [SerializeField]
    int walkwaySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in walkwayTiles)
        {
            float position = go.transform.position.x;
            go.transform.position = new Vector2(position -= walkwaySpeed * Time.deltaTime, go.transform.position.y);

            if(go.transform.position.x <= -11.77f)
            {
                go.transform.position = new Vector2(15.23f, go.transform.position.y);
            }
        }
    }
}
