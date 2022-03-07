// Written by Lawson
// Modified by Kevin Chao and Lizbeth

using System.Collections;
using UnityEngine;

public class HiskgarBehavior : MeleeBehavior
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
        followPlayer,
        attackPlayer,
        fleeingAndHealing
    }
    private HiskgarState hiskgarState;



    protected override void Awake()
    {
        base.Awake();

        hiskgarState = HiskgarState.attackPlayer;
    }


    protected override void Start()
    {
        base.Start();
        
        // Set the reference to the health manager
        self = this.gameObject.GetComponent<EnemyHealthManager>();
    }


    protected override void PerformBehavior()
    {
        switch (hiskgarState)
        {
            case HiskgarState.attackPlayer:
                // Check if health is low first
                if (self.HealthBelowPercentageThreshold(fleeHealthPercentage))
                {
                    hiskgarState = HiskgarState.fleeingAndHealing;
                }
                // Perform regular melee behavior
                base.PerformBehavior();
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
                    hiskgarState = HiskgarState.attackPlayer;
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