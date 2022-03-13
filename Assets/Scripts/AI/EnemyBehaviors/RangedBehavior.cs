// Written by Lizbeth

using System.Collections;
using UnityEngine;

public class RangedBehavior : EnemyBehaviorBase
{
    [SerializeField] private bool isPacifist = false;
    [SerializeField] private float stopAtDistance = 10f; // Enemy will stop when it is at a certain distance from target
    [SerializeField] private float attackDistanceOffset = 1f;
    [SerializeField] private float turnSpeed = 900f;

    // Attack related variables
    private MeleeDamageGiver damageGiver; // A reference to the damage giver class for applying damage
    private float attackCooldown; // Cooldown time between melee attacks
    private bool readyToCastSpell; // A bool to flag whether or not the enemy is ready to attack again
    private bool readyToFlee;

    private enum RangeState
    {
        followAndAttackTarget,
        fleeFromTarget
    }
    private RangeState state;



    protected override void Awake()
    {
        base.Awake();

        state = RangeState.followAndAttackTarget;

        // Ensure that ranged enemies are able to attack before stopping or fleeing
        if (attackDistance <= stopAtDistance)
        {
            attackDistance = stopAtDistance + attackDistanceOffset;
            if (hasFleeBehavior && (stopAtDistance <= fleeDistance))
            {
                attackDistance = fleeDistance + attackDistanceOffset;
            }
        }
    }


    protected override void Start()
    {
        base.Start();

        // Grab enemy's EnemyDamageGiver
        damageGiver = this.gameObject.GetComponent<MeleeDamageGiver>();

        // Set attack Variables
        attackCooldown = damageGiver.GetDamageOverTime();
        readyToCastSpell = true;

        // Set Flee variables
        agent.stoppingDistance = stopAtDistance;
        readyToFlee = true;
    }


    protected override void PerformEnemyBehavior()
    {
        base.PerformEnemyBehavior();

        Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        switch (state)
        {
            case RangeState.followAndAttackTarget:
                // Grab targeted player's location
                Follow(targetLocation);

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    agent.updateRotation = false;

                    Vector3 relativePosition = targetLocation - this.transform.position;
                    Quaternion rotationAngle = Quaternion.LookRotation(relativePosition, Vector3.up);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationAngle, turnSpeed);
                }

                else
                {
                    agent.updateRotation = true;
                }

                // If in attackDistance and not a pacifist, begin casting spells
                if (IsWithinAttackDistance() && readyToCastSpell && !isPacifist)
                {
                    StartCoroutine(ShootMagic());
                }

                // If range enemy has flee behavior enabled, it will run away if in flee distance
                if (IsWithinFleeDistance())
                {
                    state = RangeState.fleeFromTarget;
                    agent.updateRotation = true;
                }

                break;


            case RangeState.fleeFromTarget:
                if (readyToFlee)
                {
                    StartCoroutine(RangeIsFleeing());
                }

                else
                {
                    Flee(targetLocation);
                }

                break;
        }
    }


    private IEnumerator ShootMagic()
    {
        readyToCastSpell = false;

        this.gameObject.GetComponent<MagicCasting>().EnemyCast(); // Cast spell

        yield return new WaitForSeconds(attackCooldown);
        readyToCastSpell = true;
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
            state = RangeState.followAndAttackTarget; // Change state
        }

        readyToFlee = true;
    }
}