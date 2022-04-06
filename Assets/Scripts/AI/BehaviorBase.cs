// Written by Lawson
// Modified by Lizbeth

using UnityEngine;
using UnityEngine.AI;

// This script would need to be adjusted later
public abstract class BehaviorBase : MonoBehaviour
{
    // Detection Variables
    [SerializeField] protected float detectionRadiusBehavior; // AI's detection radius
    [SerializeField] protected float timeBetweenDetection;
    [SerializeField] protected LayerMask enemyLayerMask;  // Detect enemies layer
    [SerializeField] protected LayerMask objectLayerMask; // Detect objects layer
    [SerializeField] protected LayerMask playerLayerMask; // Detect players layer
    [SerializeField] protected LayerMask pickupLayerMask; // Detect pickups layer
    [SerializeField] protected NavMeshAgent agent; // The NavMeshAgent who has this behavior
    
    // Flee variables
    [SerializeField] private float fleeMinRadius = 30f;
    [SerializeField] private float fleeMaxRadius = 40f;

    private bool gameOver; // If false, behavior will execute. Set to true when a game ends to prevent
                           // minions from causing a game end after a player wins



    protected virtual void Awake()
    {
        EventManager.Instance.Subscribe(EventTypes.Events.GameOver, DisableBehavior);
        gameOver = false;
    }


    // Performs this behavior every frame
    private void FixedUpdate()
    {
        if (!gameOver)
        {
            PerformBehavior();
        }
    }


    // The behavior of the AI, overridden by child classes extending BehaviorBase
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


    // GENERIC BEHAVIORS
    protected void Follow(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
    }


    protected void Flee(Vector3 location)
    {
        Vector3 fleeLocation = Vector3.zero;
        Vector3 fleeDistance = this.transform.position - (location - this.gameObject.transform.position);
        Vector3 fleeVector = fleeDistance;
        bool foundFleeLocation = false;

        // Find a reachable and valid flee location
        while (!foundFleeLocation)
        {
            fleeLocation = FindValidLocation(fleeVector);

            if (fleeLocation.x != Mathf.Infinity)
            {
                foundFleeLocation = true;
            }

            else
            {
                fleeVector = CalculateRandomPointInCircle(fleeDistance, fleeMinRadius, fleeMaxRadius);
            }
        }

        agent.SetDestination(fleeVector);
    }


    private Vector3 FindValidLocation(Vector3 fleeVector)
    {
        NavMeshPath path = new NavMeshPath();

        // If path is unreachable or invalid:
        if (!agent.CalculatePath(fleeVector, path))
        {
            return Vector3.positiveInfinity;
        }

        else
        {
            return fleeVector;
        }
    }

    
    // Calculate a random point in a circle between minRange and maxRange
    protected Vector3 CalculateRandomPointInCircle(Vector3 circleCenter, float minRange, float maxRange)
    {
        Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minRange, maxRange);

        return new Vector3(point.x, 0, point.y) + circleCenter;
    }


    public float ReturnSpeed()
    {
        return agent.speed;
    }
}