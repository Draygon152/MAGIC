// Written by Lawson
// Modified by Kevin Chao and Lizbeth

using System.Collections;
using UnityEngine;

public class HiskgarBehavior : EnemyBehaviorBase
{
    [SerializeField] private int healAmount = 5; // The amount the Hiskgar heals with each tick while fleeing
    [SerializeField] private int healTime = 1;   // The amount of time between heal ticks while fleeing

    //Health percentages for switching states
    [SerializeField] private float fleeHealthPercentage = 0.2f;
    [SerializeField] private float attackHealthPercentage = 0.8f;

    private EnemyHealthManager self; // A reference to the health manager for healing
    private bool healing = false;    // A bool to flag whether or not the Hiskgar is currently healing
    private bool hasHealed = false;  // Bool flag to determine if the Hiskgar has already healed once

    // Attack Variables
    [SerializeField] private int attackHiskgarPower;
    [SerializeField] private float damageOverTimeHiskgar; // Attack player in X seconds overtime

    private DamageGiver damageGiverHiskgar; // A reference to the damage giver class for applying damage
    private bool readyToApplyDamageHiskgar; // A bool to flag whether or not the Hiskgar is ready to attack again


    private enum HiskgarState
    {
        followPlayer,
        attackPlayer,
        fleeingAndHealing
    }
    private HiskgarState state;


    protected override void Awake()
    {
        base.Awake();

        state = HiskgarState.followPlayer;
    }


    protected override void Start()
    {
        base.Start();
        
        // Set the reference to the health manager
        self = this.gameObject.GetComponent<EnemyHealthManager>();
        damageGiverHiskgar = this.gameObject.GetComponent<DamageGiver>();
        readyToApplyDamageHiskgar = true;
    }


    protected override void PerformBehavior()
    {
        // Liz's small modification:
        base.PerformBehavior();

        switch (state)
        {
            // Liz's modification:
            case HiskgarState.followPlayer:
                // Follow the player closest to enemy
                Follow(playerManager.GetPlayerLocation(currentTargetNumber).position);

                // Check if health is low first and then check attack radius
                if (self.HealthBelowPercentageThreshold(fleeHealthPercentage))
                {
                    state = HiskgarState.fleeingAndHealing;
                }
                else if (IsWithinAttackRadius())
                {
                    state = HiskgarState.attackPlayer;
                }
                
                break;

            case HiskgarState.attackPlayer:
                // Check if health is low first
                if (self.HealthBelowPercentageThreshold(fleeHealthPercentage))
                {
                    state = HiskgarState.fleeingAndHealing;
                }
                else if (!IsWithinAttackRadius())
                {
                    state = HiskgarState.followPlayer; // If player not within attack radius, change state to following
                }

                // Attack closest player if ready
                if (readyToApplyDamageHiskgar)
                {
                    StartCoroutine(CauseTargetDamage());
                }

                break;
                // Check if health is below flee health percentage
                // if so switch to fleeing

            case HiskgarState.fleeingAndHealing:
                // when fleeing from the player, flee from closest player and heal
                Flee(playerManager.GetPlayerLocation(currentTargetNumber).position);
                // heal since you are fleeing
                if (!hasHealed && !healing)
                {
                    StartCoroutine(HealSelf());
                }
            
                // Check if health is above attack health percentage
                // if so switch to attacking
                if (self.HealthAbovePercentageThreshold(attackHealthPercentage))
                {
                    state = HiskgarState.followPlayer;
                    hasHealed = true;
                }
                break;
        } // end swtich (state)
    }


    private IEnumerator HealSelf()
    {
        // mark self as healing
        healing = true; 

        // heal self
        self.GainHealth(healAmount);

        // cooldown before next heal, when done mark self as not healing
        yield return new WaitForSeconds(healTime);
        healing = false;
    }

    // Apply damage
    private IEnumerator CauseTargetDamage()
    {
        readyToApplyDamageHiskgar = false;

        PlayerHealthManager targetHealthManager = playerManager.GetPlayer(currentTargetNumber).gameObject.GetComponent<PlayerHealthManager>();
        if (targetHealthManager != null && damageGiverHiskgar != null)
        {
            damageGiverHiskgar.DamageTarget(targetHealthManager, attackHiskgarPower);
        }

        yield return new WaitForSeconds(damageOverTimeHiskgar);
        readyToApplyDamageHiskgar = true;
    }
}