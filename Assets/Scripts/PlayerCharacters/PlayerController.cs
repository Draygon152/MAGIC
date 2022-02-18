// Written by Marc
// Modified by Kevin Chao and Lawson

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed; // Controls the movement speed of player.
    [SerializeField] private float turnSpeed; // Controls the turn speed of player.

    private PlayerInput playerControls; // A reference to the PlayerInput component that handles player input

    Vector3 inputDirection; //The vector for moving the player



    private void Awake()
    {
        Debug.Log("Player controller awake");
        // set the reference to the PlayerInput component
        playerControls = this.GetComponent<PlayerInput>();

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }

    //Update the inputDirection vector only when the controls of
    //the apporiate control scheme are changed
    private void OnMove(InputValue value)
    {
        // Gathers our input from WASD Keys, set in the Input Manager system in Unity.
        // Since we're in Orthographic View:
        // In here, the X-axis should refer to our Right Input Actions "A to move left, D to move right in the X-axis".
        // ...and Z-axis would refer to our Y-axis movements "W to move up, S to move down".
        inputDirection = value.Get<Vector3>();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        // Using rigidbody component, move our player. Uses rb.velocity to maintain collisions properly
        rb.velocity = transform.forward * inputDirection.normalized.magnitude * moveSpeed;
    }
    

    private void Turn()
    {
        if (inputDirection != Vector3.zero)
        {

            // This is to have our W,S keys point upwards and downwards in Orthographic view; A,S keys point diagonal.
            Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 changedInputDirection = matrix.MultiplyPoint3x4(inputDirection);

            // Gets the relative position to our current position to target destination.
            Vector3 relativePosition = (transform.position + changedInputDirection) - transform.position;

            // ...and make that a rotation angle (to look to)
            // Vector3.up tells Unity to rotate us around the "Up" axis.
            Quaternion rotationAngle = Quaternion.LookRotation(relativePosition, Vector3.up); 

            // Update player rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationAngle, turnSpeed); 
        }
    }
}
