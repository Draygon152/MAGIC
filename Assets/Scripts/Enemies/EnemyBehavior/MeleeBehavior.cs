// Written by Lizbeth

using System.Collections;
using UnityEngine;

public class MeleeBehavior : EnemyBehaviorBase
{
    private CollisionDamageGiver damageGiver; // A reference to the damage giver class for applying damage
    private int attackPower;
    private float attackCooldown; // Cooldown time between melee attacks
    private bool readyToApplyDamage; // A bool to flag whether or not the enemy is ready to attack again
    private bool readyToFlee;
    
    protected enum MeleeState
    {
        followTarget,
        attackTarget,
        fleeFromTarget
    }
    protected MeleeState meleeState;


    protected override void Awake()
    {
        base.Awake();

        meleeState = MeleeState.followTarget;
    }


    protected override void Start()
    {
        base.Start();

        // TODO: Rename CollisionDamageGiver class for accuracy
        // Grab enemy's EnemyDamageGiver
        damageGiver = this.gameObject.GetComponent<CollisionDamageGiver>(); 

        // Set attack Variables
        attackPower = damageGiver.CurrentDamage();
        attackCooldown = damageGiver.GetDamageOverTime();
        readyToApplyDamage = true;

        // Set Flee variables
        readyToFlee = true;
    }


    protected override void PerformEnemyBehavior()
    {
        base.PerformEnemyBehavior();

        Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        switch (meleeState)
        {
            case MeleeState.followTarget:
                // Grab targeted player's location
                Follow(targetLocation);

                // Change to attack state when target is nearby
                if (IsWithinAttackDistance())
                {
                    meleeState = MeleeState.attackTarget;
                }

                break;
            
            case MeleeState.attackTarget:
                // If target not within radius, change state
                if (!IsWithinAttackDistance())
                {
                    meleeState = MeleeState.followTarget;
                }
                
                // Target is within attack radius, apply damage overtime
                if (readyToApplyDamage)
                {
                    StartCoroutine(CauseTargetDamage());
                    
                    // If melee enemy has flee behavior, it will hit the target once and run away
                    if (IsWithinFleeDistance())
                    {
                        meleeState = MeleeState.fleeFromTarget;
                    }
                }
                break;

            case MeleeState.fleeFromTarget:
                if (readyToFlee)
                {
                    StartCoroutine(MeleeIsFleeing());
                }

                else
                {
                    Flee(targetLocation);
                }
                break;
        }
    }

    
    private IEnumerator CauseTargetDamage()
    {
        readyToApplyDamage = false;

        PlayerHealthManager targetHealthManager = playerManager.GetPlayer(currentTargetNumber).gameObject.GetComponent<PlayerHealthManager>();
        if (targetHealthManager != null && damageGiver != null)
        {
            damageGiver.DamageTarget(targetHealthManager, attackPower); 
        }

        yield return new WaitForSeconds(attackCooldown);
        readyToApplyDamage = true;
    }


    private IEnumerator MeleeIsFleeing()
    {
        readyToFlee = false;
        Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        Flee(targetLocation);

        yield return new WaitForSeconds(fleeCooldown);

        if (!IsWithinAttackDistance())
        {
            meleeState = MeleeState.followTarget;
        }

        else
        {
            meleeState = MeleeState.attackTarget;
        }

        readyToFlee = true;
    }
}