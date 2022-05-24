// Written by Lawson McCoy
// Modified by Kevin Chao

using UnityEngine;

public class HealthBillboard : MonoBehaviour
{
    // LateUpdate is required so that the camera moves before rotating the health bar
    protected virtual void LateUpdate()
    {
        TurnTowardsMainCamera();
    }


    protected virtual void TurnTowardsMainCamera()
    {
        this.transform.LookAt(this.transform.position + CameraSystem.Instance.gameObject.transform.rotation * Vector3.forward);
    }
}