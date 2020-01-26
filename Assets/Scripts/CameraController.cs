using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable 649
    // Reference to the player - the object the camera needs to follow
    [SerializeField]
    PlayerController player;
    // The distance the player can move without the camera following
    [SerializeField]
    float followOffset;
#pragma warning restore 649

    // The boundary around the player
    float boundry;
    // The cameras original position
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // calculate the boundary
        boundry = CalculateBoundary();

        // the camreas initial position
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The position of the object the camera is following
        Vector2 ObjectToFollowPos = player.transform.position;

        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * ObjectToFollowPos.y);

        Vector3 newPosition = transform.position;

        // If the player moves out of the boundary apply the players Y position
        // to the cameras Y position, otherwise set the camera to its origin position
        if (Mathf.Abs(yDifference) >= boundry)
        {
            newPosition.y = ObjectToFollowPos.y;
        } 
        else
        {
            newPosition = originalPosition;
        }

        // move the camera towards its postion each frame
        transform.position = Vector3.MoveTowards(transform.position, newPosition,
            player.GetComponent<Rigidbody2D>().velocity.magnitude * Time.deltaTime);
    }

    public float CalculateBoundary()
    {
        // The aspect ratio of the camera
        Rect aspectRatio = Camera.main.pixelRect;

        // boundary height set to the camera orthographic size
        float boundaryHeight = Camera.main.orthographicSize;

        // reduce the boundary height
        boundaryHeight -= followOffset;

        return boundaryHeight; 
    }
}
