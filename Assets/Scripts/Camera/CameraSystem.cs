using UnityEngine;

//written by Marc
public class CameraSystem : MonoBehaviour
{

    [Range(0.01f, 1.0f)] //Adds a range slider in the Inspector.
    public float smoothValue = 0.2f; //Float variable for smooth camera movement.

    public Transform player; //Reference to Transform component of the player.


    public void FixedUpdate()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        Vector3 newPos = player.position; //Gets the targeted new position of the camera

        transform.position = Vector3.Lerp(transform.position, newPos, smoothValue); //Smoothly moves the camera that follows the player.
    }
}
