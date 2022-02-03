// Written by Marc

using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Range(0.01f, 1.0f)] // Adds a range slider in the Inspector.
    [SerializeField] private float smoothValue = 0.2f; // Float variable for smooth camera movement.

    public Transform player; // Reference to Transform component of the player.

    // TODO: Make CameraSystem a singleton

    private void FixedUpdate()
    {
        MoveCamera();
    }


    private void MoveCamera()
    {
        Vector3 newPos = player.position; // Gets the targeted new position of the camera

        transform.position = Vector3.Lerp(transform.position, newPos, smoothValue); // Smoothly moves the camera that follows the player.
    }
}
