using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 8f; //Controls the movement speed of player.
    [SerializeField] private float turnSpeed = 360f; //Controls the turn speed of player.

    private PlayerControls inputControls; //The action map that is reading in the player's input
    private Vector3 inputDirection; //Vector to gather our WASD keys input.

    private void Awake()
    {
        //init the player controls action map
        inputControls = new PlayerControls();
    }

    private void OnEnable()
    {
        //Enabling control for the player
        inputControls.Enable();
    }

    private void OnDisable()
    {
        //Disabling control for the player
        inputControls.Disable();
    }

    private void Update()
    {
        //Gathers our input from WASD Keys, set in the Input Manager system in Unity. 
        inputDirection = new Vector3(inputControls.Move.Forwards.ReadValue<float>(), 0, inputControls.Move.Right.ReadValue<float>());
        Turn();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Using rigidbody component, move our player.
        rb.MovePosition(transform.position + (transform.forward * inputDirection.normalized.magnitude) * moveSpeed * Time.deltaTime);
    }
    
    void Turn()
    {
        if (inputDirection != Vector3.zero)
        {

            //This is to have our W,S keys point upwards and downwards in Orthographic view; A,S keys point diagonal.
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var changedInputDirection = matrix.MultiplyPoint3x4(inputDirection);


            //Gets the relative position to our current position to target destination.
            var relativePosition = (transform.position + changedInputDirection) - transform.position;
            //...and make that a rotation angle (to look to).
            var rotationAngle = Quaternion.LookRotation(relativePosition, Vector3.up); //Vector3.up tells Unity to rotate us around the Up axis.

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationAngle, turnSpeed * Time.deltaTime); //Rotate our player smoothly.
        }
        
    }
}
