// Written by Lizbeth

using System.Collections;
using UnityEngine;

public class RangeBehavior : EnemyBehaviorBase
{
    [SerializeField] private float stopAtDistance = 1f; // Enemy will stop when it is at a certain distance from target

    private bool readyToFlee;

    private enum RangeState
    {
        followTarget,
        fleeFromTarget
    }
    private RangeState state;



    protected override void Awake()
    {
        base.Awake();

        state = RangeState.followTarget;
    }


    protected override void Start()
    {
        base.Start();

        agent.stoppingDistance = stopAtDistance;
        readyToFlee = true;
    }


    protected override void PerformBehavior()
    {
        base.PerformBehavior();

        Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        switch (state)
        {
            case RangeState.followTarget:
                
                // Grab targeted player's location
                Follow(targetLocation);

                // If range enemy has flee behavior enabled, it will run away if in flee distance
                if (IsWithinFleeDistance())
                {
                    state = RangeState.fleeFromTarget;
                }
                break;

            case RangeState.fleeFromTarget:
                if (readyToFlee)
                {
                    StartCoroutine(RangeIsFleeing());
                }

                else
                {
                    this.gameObject.GetComponent<MagicCasting>().EnemyCast();
                    Flee(targetLocation);
                }
                break;
        }
    }


    private IEnumerator RangeIsFleeing()
    {
        readyToFlee = false;
        agent.stoppingDistance = 0; // Set to zero because enemy would not be able to run away if the target's agent is close
        Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        Flee(targetLocation);

        yield return new WaitForSeconds(fleeCooldown);

        if (!IsWithinFleeDistance())
        {
            agent.stoppingDistance = stopAtDistance; // Reset enemy's stopping distance
            state = RangeState.followTarget; // Change state
        }

        readyToFlee = true;
    }
}