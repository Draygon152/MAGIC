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
    [SerializeField] protected LayerMask pickupLayerMask; // Detect pickups layer
    protected NavMeshAgent agent; //The NavMeshAgent who has this behavior
    private bool gameOver; // If false, behavior will execute. Set to true when a game ends to prevent
                           // minions from causing a game end after a player wins

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
        Vector3 fleeVector = location - this.gameObject.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    protected void Follow(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
    }
}
  