// Written by Marc Hagoriles
// Modified by Kevin Chao

using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float smoothValue = 0.2f;    // Float variable for smooth camera movement.
    [SerializeField] private float minCamSize = 5f;       // This is to limit the camera to have a minimum ortho cam size for the zoom.
    [SerializeField] private float screenEdgeBuffer = 6f; // Space between the top/bottom most target and the screen edge.
    
    private float zoomSpeed;            // For SmoothDamp function.
    private Vector3 camSpeed;           // For SmoothDamp function, velocity of Camera moving.
    private Camera cam;                 // Reference to the Camera component
    private List<Transform> targetList; // This list should contain all the player targets in the scene.

    // Make the CameraSystem a Singleton.
    public static CameraSystem Instance
    {
        get;
        private set;
    }



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            cam = GetComponentInChildren<Camera>();
            targetList = new List<Transform>();
        }
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    private void FixedUpdate()
    {
        if (targetList.Count > 0)
        {
            MoveCamera();
            Zoom();
        }
    }


    // Get the center position of all players in the scene.
    private Vector3 GetCenterPos()
    {
        Vector3 avgPos = new Vector3();
        int numTargets = 0;

        // If there is only one player, just return its position and fixate the Camera on it, per usual.
        if (targetList.Count == 1)
        {
            avgPos = targetList[0].position;
        }

        // Otherwise, calculate camera position based on all players
        else
        {
            // Depending on how many players there are, calculate its average position
            for (int i = 0; i < targetList.Count; i++)
            {
                // Add the position of each target into avgPos and increment the numTargets for use later.
                avgPos += targetList[i].position;
                numTargets++;
            }

            // If there are targets, get its average position.
            if (numTargets > 0)
            {
                avgPos /= numTargets;
            }
        }
        
        return avgPos;
    }


    private void MoveCamera()
    {
        // Gets the targeted new position of the camera, update when multiple players implemented
        Vector3 newPos = GetCenterPos();

        // transform.position = Vector3.Lerp(transform.position, newPos, smoothValue); // Smoothly moves the camera that follows the player.
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref camSpeed, smoothValue);
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


    // Removes a target from the List.
    public void RemoveFrameTarget(Transform removedTarget)
    {
        targetList.Remove(removedTarget);
    }

    public void ClearCameraFrame()
    {
        targetList.Clear();
    }


    private float FindZoomSize()
    {
        // Find the position of the camera moving toward its target position in its local space.
        Vector3 camLocalPos = transform.InverseTransformPoint(GetCenterPos());

        float size = 0f; // Initialize the calculation of camera's zoom size to be 0.
        Vector3 targetLocalPos, camToTarget;

        for (int i = 0; i < targetList.Count; i++)
        {
            // Find the target's position in the camera's local space.
            targetLocalPos = transform.InverseTransformPoint(targetList[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            camToTarget = targetLocalPos - camLocalPos;

            // Choose the largest out of the current size and the distance of the tank in the y direction (up or down).
            size = Mathf.Max(size, Mathf.Abs(camToTarget.y));

            // Same for above, but in the x direction (left to right)
            size = Mathf.Max(size, Mathf.Abs(camToTarget.x) / cam.aspect);
        }

        // Add the buffer.
        size += screenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, minCamSize);

        return size;
    }


    // Smoothly zoom the camera in and out depending on the position of the players.
    private void Zoom()
    {
        float zoomSize = FindZoomSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomSize, ref zoomSpeed, smoothValue);
    }


    // This function is to set the Camera's starting position, not smoothly.
    // Called in GameManager's StartGame() function.
    public void StartingCamPos()
    {
        Vector3 startPos = GetCenterPos();
        transform.position = startPos;
        cam.orthographicSize = FindZoomSize();
    }
}