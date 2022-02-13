// Written by Marc
// Modified by Kevin Chao

using System.Collections.Generic;
using UnityEngine;

// TODO: Make CameraSystem a singleton
public class CameraSystem : MonoBehaviour
{
    [Range(0.01f, 1.0f)] // Adds a range slider in the Inspector.
    [SerializeField] private float smoothValue = 0.2f; // Float variable for smooth camera movement.

    private List<Transform> targetList;

    

    private void FixedUpdate()
    {
        MoveCamera();
    }


    private void MoveCamera()
    {
        // Gets the targeted new position of the camera, update when multiple players implemented
        Vector3 newPos = targetList[0].position; 

        transform.position = Vector3.Lerp(transform.position, newPos, smoothValue); // Smoothly moves the camera that follows the player.
    }


    public Transform GetTransform()
    {
        return transform;
    }


    // Adds Transform of additional target to keep in frame
    public void AddFrameTarget(Transform addedTarget)
    {
        targetList.Add(addedTarget);
    }


    public void RemoveFrameTarget(Transform removedTarget)
    {
        targetList.Remove(removedTarget);
    }
}