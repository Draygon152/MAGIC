// Written by Lizbeth
// Modified by Lawson and Kevin Chao

using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private bool isMinion;



    protected override void Start()
    {
        // Call HealthManager start first
        base.Start();

        //Initialize health bar
        healthBar.InitializeHealthBar(maxHealth);

        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, Despawn);
    }


    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(EventTypes.Events.ResetGame, Despawn);
    }


    public override void GainHealth(int healAmount)
    {
        base.GainHealth(healAmount);

        healthBar.UpdateHealth(currentHealth);
    }


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Update the health bar
        healthBar.UpdateHealth(currentHealth);

        // If health becomes 0 or less, enemy destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement option to spawn a higher-tier spell drop at location of death

            // Minions do not count towards the enemy count, so should not decrease the counter
            if (!isMinion)
                EventManager.Instance.Notify(EventTypes.Events.EnemyDeath);

            Destroy(gameObject);
        }
    }


    // Despawn the enmey when the game is over
    private void Despawn()
    {
        Destroy(gameObject);
    }
}