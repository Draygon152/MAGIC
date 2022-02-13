// Written by Liz
// Modified by Lawson

using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    [SerializeField] private EnemyHealthBar healthBar;



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


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Update the health bar
        healthBar.UpdateHealth(currentHealth);
        
        Debug.Log($"Health of {gameObject.name} after damage: {currentHealth}");

        // If health becomes 0 or less, enemy destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement option to spawn a higher-tier spell drop at location of death

			EventManager.Instance.Notify(EventTypes.Events.EnemyDeath);
            Destroy(gameObject);
        }
    }


    // Despawn the enmey when the game is over
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
