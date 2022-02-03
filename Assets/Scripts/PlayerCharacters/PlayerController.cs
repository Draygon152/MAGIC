using UnityEngine;

// Written primarily by Marc
// Modify slightly by Lawson
// Debugged and collisions fixed by Kevin Chao
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 8f;   // Controls the movement speed of player.
    [SerializeField] private float turnSpeed = 720f; // Controls the turn speed of player.

    private PlayerControls PlayerControlsMap; // The Action Map that we created (Called PlayerControls) that is reading in the player's input
    private Vector3 inputDirection;           // Vector to gather our WASD keys input.



    private void Awake()
    {
        // init the player controls action map
        PlayerControlsMap = new PlayerControls();
    }


    private void OnEnable()
    {
        // Enabling control for the player
        PlayerControlsMap.Enable();
    }


    private void OnDisable()
    {
        // Disabling control for the player
        PlayerControlsMap.Disable();
    }


    private void FixedUpdate()
    {
        // Gathers our input from WASD Keys, set in the Input Manager system in Unity.
        // Since we're in Orthographic View:
        // In here, the X-axis should refer to our Right Input Actions "A to move left, D to move right in the X-axis".
        // ...and Z-axis would refer to our Y-axis movements "W to move up, S to move down".
        inputDirection = new Vector3(PlayerControlsMap.Move.Right.ReadValue<float>(), 0, PlayerControlsMap.Move.Forward.ReadValue<float>());

        Move();
        Turn();
    }


    void Move()
    {
        // Using rigidbody component, move our player. Uses rb.velocity to maintain collisions properly
        rb.velocity = transform.forward * inputDirection.normalized.magnitude * moveSpeed;
    }
    

    void Turn()
    {
        if (inputDirection != Vector3.zero)
        {

            // This is to have our W,S keys point upwards and downwards in Orthographic view; A,S keys point diagonal.
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var changedInputDirection = matrix.MultiplyPoint3x4(inputDirection);


            // Gets the relative position to our current position to target destination.
            var relativePosition = (transform.position + changedInputDirection) - transform.position;
            // ...and make that a rotation angle (to look to).
            var rotationAngle = Quaternion.LookRotation(relativePosition, Vector3.up); // Vector3.up tells Unity to rotate us around the Up axis.

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationAngle, turnSpeed * Time.deltaTime); // Rotate our player smoothly.
        }
    }
}
