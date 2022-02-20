using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiskgarBehavior : EnemyBehaviorBase
{
    [SerializeField] private int healRate = 5; //The heal rate of the enemy when fleeing

    //Health percentages for switching states
    [SerializeField] private float fleeHealthPercentage = 0.2f;
    [SerializeField] private float attackHealthPercentage = 0.8f;

    private EnemyHealthManager self; //A reference to the health manager for healing

    private enum HiskgarState
    {
        attackPlayer,
        fleeingAndHealing
    }
    private HiskgarState state;

    private void Awake()
    {
        state = HiskgarState.attackPlayer;
    }

    private void Start()
    {
        //Set the reference to the health manager
        self = this.gameObject.GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (state)
        {
        case HiskgarState.attackPlayer:
            //when attacking the player follow the closest player 
            Follow(playerManager.GetPlayerLocation(currentTargetNumber).position);

            //Check if health is below flee health percentage
            //if so switch to fleeing
            if (self.HealthBelowPercentageThreshold(fleeHealthPercentage))
            {
                state = HiskgarState.fleeingAndHealing;
            }
            break;

        case HiskgarState.fleeingAndHealing:
            //when fleeing from the player, flee from closest player and heal
            Flee(playerManager.GetPlayerLocation(currentTargetNumber).position);
            //heal since you are fleeing
            self.GainHealth(healRate); 
            
            //Check if health is above attack health percentage
            //if so switch to attacking
            if (self.HealthAbovePercentageThreshold(attackHealthPercentage))
            {
                state = HiskgarState.attackPlayer;
            }
            break;
        }//end swtich (state)
    }//end FixedUpdate
}
