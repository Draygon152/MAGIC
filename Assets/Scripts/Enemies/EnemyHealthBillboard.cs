// Written by Lawson McCoy
// Modified by Kevin Chao

using UnityEngine;

public class EnemyHealthBillboard : MonoBehaviour
{
    [SerializeField] private Transform cameraRef; // A reference to the camera orientation the health bar needs to face



    // LateUpdate is required so that the camera moves before rotating the health bar
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cameraRef.rotation * Vector3.forward);
    }


    public void SetCamera(Transform cameraToLookAt)
    {
        cameraRef = cameraToLookAt;
    }
}