// Written by Lizbeth
using System.Collections;
using UnityEngine;

public class MeleeBehavior : EnemyBehaviorBase
{
    [SerializeField] private int attackPower;
    [SerializeField] private float damageOverTime; // Attack player in X seconds overtime

    private DamageGiver damageGiver; // A reference to the damage giver class for applying damage
    private bool readyToApplyDamage; // A bool to flag whether or not the Hiskgar is ready to attack again

    private enum MeleeState
    {
        followTarget,
        attackTarget
    }
    private MeleeState state;

    protected override void Start()
    {
        base.Start();
        damageGiver = this.gameObject.GetComponent<DamageGiver>();
        readyToApplyDamage = true;
    }

    protected override void PerformBehavior()
    {
        base.PerformBehavior();
        
        switch (state)
        {
            case MeleeState.followTarget:
                // Grab targeted player's location
                Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
                Follow(targetLocation);

                // Change to attack state when target is nearby
                if (IsWithinAttackRadius())
                {
                    state = MeleeState.attackTarget;
                }

                break;
            
            case MeleeState.attackTarget:
                // If target not within radius, change state
                if (!IsWithinAttackRadius())
                {
                    state = MeleeState.followTarget;
                }

                // Target is within attack radius, apply damage overtime
                if (readyToApplyDamage)
                {
                    StartCoroutine(CauseTargetDamage());
                }
                break;
        }
    }

    
    private IEnumerator CauseTargetDamage()
    {
        readyToApplyDamage = false;

        PlayerHealthManager targetHealthManager = playerManager.GetPlayer(currentTargetNumber).gameObject.GetComponent<PlayerHealthManager>();
        if(targetHealthManager != null && damageGiver != null)
        {
            damageGiver.DamageTarget(targetHealthManager, attackPower); 
        }

        yield return new WaitForSeconds(damageOverTime);
        readyToApplyDamage = true;
    }
}
