// Written by Liz
// Modified by Kevin Chao

using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private Rigidbody objRigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float distanceFromTarget;
    [SerializeField] private float tooCloseToTarget;
    [SerializeField] private bool cowardly;

    private Transform target;



    private void Start()
    {
        objRigidbody = GetComponent<Rigidbody>();

        // Access the player prefab clones
        target = PlayerManager.Instance.GetPlayerLocation(PlayerManager.PLAYER_1);
        
        // If tooCloseToTarget is greater than distanceFromTarget, it will adjust the distance to be
        // greater than tooCloseToTarget to avoid object from jittering. This occurs ONLY if cowardly is enabled.
        if (distanceFromTarget <= tooCloseToTarget && cowardly)
            distanceFromTarget = tooCloseToTarget + 0.5f;
    }


    private void FixedUpdate()
    {
        LookAtTarget();
        GoToTarget();
    }


    // LookAtTarget() rotates object to face its target.
    private void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotate = Quaternion.LookRotation(relativePos, Vector3.up);  // Rotate object to face target.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, turnSpeed * Time.deltaTime); // Rotate object smoothly.
        }
    }


    // GoToTarget() moves object towards its target at a certain distance.
    private void GoToTarget()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance > distanceFromTarget)
            {
                // If distance is greater than distanceFromTarget, object will move towards its target
                // because it is too far.
                Vector3 newPos = transform.position + transform.forward * speed * Time.deltaTime;
                objRigidbody.MovePosition(newPos);
            }

            else if (cowardly)
            {
                if (distance < tooCloseToTarget)
                {
                    // If distance is less than tooCloseToTarget, object will move far from its target
                    // because it is too close. This occurs ONLY if cowardly is enabled.
                    Vector3 backwardDirection = transform.forward;
                    backwardDirection.x *= -1;
                    backwardDirection.z *= -1;
                    Vector3 newPos = transform.position + backwardDirection * speed * Time.deltaTime;
                    objRigidbody.MovePosition(newPos);
                }
            }
        }
    }
}