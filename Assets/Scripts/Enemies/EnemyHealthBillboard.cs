// Written by Lawson McCoy
// Modified by Kevin Chao

using UnityEngine;

public class EnemyHealthBillboard : MonoBehaviour
{
    // LateUpdate is required so that the camera moves before rotating the health bar
    private void LateUpdate()
    {
        transform.LookAt(transform.position + CameraSystem.Instance.gameObject.transform.rotation * Vector3.forward);
    }
}