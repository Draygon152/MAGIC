using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyBehaviorBase : BehaviorBase
{
    [SerializeField] protected NavMeshAgent agent;
    override protected void PerformBehavior()
    {
        
    }

    protected void Follow(Vector3 location)
    {
        agent.SetDestination(location);
    }

    protected void Flee(Vector3 location)
    {
        Vector3 distance = location - this.gameObject.transform.position;
        Vector3 fleeVector = this.transform.position - distance;
        agent.SetDestination(fleeVector);
    }
    
    // Should move to BehaviorBase but i got lazy
    protected Collider[] DetectLayerWithinRadius(Vector3 center, float detectRadius, LayerMask layer)
    {
        return Physics.OverlapSphere(center, detectRadius, layer);  
    }
}
