// Written by Lawson
// Modified by Kevin Chao

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


    private enum HiskgarState
    {
        attackPlayer,
        fleeingAndHealing
    }
    private HiskgarState state;



    protected override void Awake()
    {
        base.Awake();

        state = HiskgarState.attackPlayer;
    }


    protected override void Start()
    {
        base.Start();

        // Set the reference to the health manager
        self = this.gameObject.GetComponent<EnemyHealthManager>();
    }


    protected override void PerformBehavior()
    {
        switch (state)
        {
            case HiskgarState.attackPlayer:
                // when attacking the player follow the closest player 
                Follow(playerManager.GetPlayerLocation(currentTargetNumber).position);

                // Check if health is below flee health percentage
                // if so switch to fleeing
                if (self.HealthBelowPercentageThreshold(fleeHealthPercentage))
                {
                    state = HiskgarState.fleeingAndHealing;
                }
                break;

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
                    state = HiskgarState.attackPlayer;
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
}