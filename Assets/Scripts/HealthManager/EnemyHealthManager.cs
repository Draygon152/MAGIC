using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log($"Health of {gameObject.tag} after damage: {currentHealth}");

        // If health becomes 0 or less, enemy destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement option to spawn a higher-tier spell drop at location of death

            Destroy(gameObject);

            //Fire the enemy death event to inform other system of the death of an enemy
            EventManager.Instance.Notify(Event.EventTypes.EnemyDeath);
        }
    }
}
