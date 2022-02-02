using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBillboard : MonoBehaviour
{
    [SerializeField] private Transform camera; //A reference to the camera the health bar needs to face

    //LateUpdate is required so that the camera moves before rotating the health bar
    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + camera.rotation * Vector3.forward);
    }

    public void SetCamera(Transform cameraToLookAt)
    {
        camera = cameraToLookAt;
    }
}
