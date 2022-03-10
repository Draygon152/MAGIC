// Written by Lawson

using UnityEngine;
using UnityEngine.AI;

// This script would need to be adjusted later
public abstract class BehaviorBase : MonoBehaviour
{
    // Detection Variables
    // Change detectionRadiusBehavior to detectionRadius after my (Liz) new enemy branch is merged
    [SerializeField] protected float detectionRadiusBehavior; // AI's detection radius
    [SerializeField] protected float timeBetweenDetection;
    [SerializeField] protected LayerMask enemyLayerMask; // Detect enemies layer
    [SerializeField] protected LayerMask objectLayerMask; // Detect objects layer
    [SerializeField] protected LayerMask playerLayerMask; // Detect players layer
    [SerializeField] protected LayerMask environment; //Detect environement layer

    protected NavMeshAgent agent; //The NavMeshAgent who has this behavior
    private bool gameOver; // If false, behavior will execute. Set to true when a game ends to prevent
                           // minions from causing a game end after a player wins

    [SerializeField] private float fleeMinRadius = 30f;
    [SerializeField] private float fleeMaxRadius = 40f;
    [SerializeField] private float wallLookAhead = 40f;
    [SerializeField] private float angelIncrementSteps = 5.0f; //This size of the steps in degrees to take
                                                               //in order to find a location to go to

    //Performs this behavior every frame
    private void FixedUpdate()
    {
        if (!gameOver)
        {
            PerformBehavior();
        }
    }

    protected virtual void Awake()
    {
        EventManager.Instance.Subscribe(EventTypes.Events.GameOver, DisableBehavior);
        gameOver = false;
    }

    virtual protected void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    //The behavior of the AI
    protected abstract void PerformBehavior();

    protected Collider[] DetectLayerWithinRadius(Vector3 center, float detectRadius, LayerMask layer)
    {
        return Physics.OverlapSphere(center, detectRadius, layer);  
    }

    private void DisableBehavior()
    {
        gameOver = true;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(EventTypes.Events.GameOver, DisableBehavior);
    }

    //BEHAVIORS

    protected void Flee(Vector3 location)
    {
        Vector3 fleeLocation = Vector3.zero;
        Vector3 fleeDistance = this.transform.position - (location - this.gameObject.transform.position);
        Vector3 fleeVector = fleeDistance;
        bool foundFleeLocation = false;

        bool lookRight = true;
        float angelOfRotation = angelIncrementSteps;
        RaycastHit pathInfo; //data from the report of the raycast
        Vector3 checkIfDirectionIsClear = this.transform.forward; //The direction you are checking is clear
        Quaternion rotationTransformationMatrix; //The transformation matrix for rotating the vector

        // Find a reachable and valid flee location
        if (!IsValidLocation(fleeVector))
        {
            // location is not reachable, find one that is
            while (Physics.Raycast(this.transform.position, checkIfDirectionIsClear, wallLookAhead, environment))
            {
                //Direction is not clear, rotate and check again
                if (lookRight)
                { 
                    //rotate to the right
                    rotationTransformationMatrix = Quaternion.AngleAxis(angelOfRotation, Vector3.up); //rotate cw (right) AngelOfRotation degrees about up
                    checkIfDirectionIsClear = rotationTransformationMatrix * this.transform.forward;

                    //Next time look to the left
                    lookRight = false;
                }
                else
                {
                    //rotate to the left
                    rotationTransformationMatrix = Quaternion.AngleAxis(-1 * angelOfRotation, Vector3.up); //rotate ccw (left) AngelOfRotation degrees about up
                    checkIfDirectionIsClear = rotationTransformationMatrix * this.transform.forward;

                    //Next time look to the right
                    lookRight = true;

                    //Since both right and left directions have been checked for this angel, increment to the next angel
                    angelOfRotation += angelIncrementSteps;
                } //end else (if (lookRight))
            } //end while (Physics.Raycast(this.transform.position, checkIfDirectionIsClear, wallLookAhead, environement))

            //now that a direction that is clear has been found, use it to update fleeVector
            Debug.Log($"Angel of rotation: {angelOfRotation}");
            Debug.Log($"Turning the right: {lookRight}");
            fleeVector = checkIfDirectionIsClear * wallLookAhead;
        }
        // while (!foundFleeLocation)
        // {
        //     fleeLocation = FindValidLocation(fleeVector);
        //     bool test = fleeLocation.x != Mathf.Infinity;
        //     if (fleeLocation.x != Mathf.Infinity)
        //     {
        //         foundFleeLocation = true;
        //     }
        //     else
        //     {
        //         fleeVector = CalculateRandomPointInCircle(fleeDistance, fleeMinRadius, fleeMaxRadius);
        //     }
        // }

        agent.SetDestination(fleeVector);
    }

    // Calculate a random point in a circle between minRange and maxRange
    protected Vector3 CalculateRandomPointInCircle(Vector3 circleCenter, float minRange, float maxRange)
    {
        Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minRange, maxRange);
        return new Vector3(point.x, 0, point.y) + circleCenter;
    }

    private bool IsValidLocation(Vector3 fleeVector)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(fleeVector, path);

        // If path is unreachable or invalid:
        if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void Follow(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
    }
}
  