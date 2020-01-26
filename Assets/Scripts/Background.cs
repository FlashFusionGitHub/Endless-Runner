using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
#pragma warning disable 649
    // Reference to the background images
    [SerializeField]
    GameObject[] backgroundImages;
    // The backgrounds max move speed
    [SerializeField]
    int backgroundSpeed;
    [SerializeField]
    int backgroundImageLength;
#pragma warning restore 649

    // Getter and Setter for stopwalking boolean
    public int BackgroundSpeed { get { return backgroundSpeed; } set { backgroundSpeed = value; } }

    Vector3 firstbackgroundImagePosition, lastbackgroundImagePosition;

    // Start is called before the first frame update
    void Awake()
    {
        firstbackgroundImagePosition = backgroundImages[0].transform.position;
        lastbackgroundImagePosition = backgroundImages[1].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the position of every walkway tile each frame
        foreach (GameObject go in backgroundImages)
        {
            float position = go.transform.position.x;
            go.transform.position = new Vector2(position -= backgroundSpeed * Time.deltaTime, go.transform.position.y);

            RecycleBackgroundImage(go);
        }
    }

    // Sets the position of the last tile to the fornt of the queue
    void RecycleBackgroundImage(GameObject go)
    {
        if (go.transform.position.x <= firstbackgroundImagePosition.x)
        {
            go.transform.position = new Vector2(lastbackgroundImagePosition.x + backgroundImageLength, go.transform.position.y);
        }
    }
}
