using UnityEngine;

//Written primarily by Liz
//Modify slightly by Lawson
public class EnemyHealthManager : HealthManager
{
    [SerializeField] private EnemyHealthBar healthBar;

    void Start()
    {
        //Call HealthManager start first
        base.Start();

        //Initialize health bar
        healthBar.InitializeHealthBar(maxHealth);
    }

    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        //update the health bar
        healthBar.UpdateHealth(currentHealth);
        
        Debug.Log($"Health of {gameObject.tag} after damage: {currentHealth}");

        // If health becomes 0 or less, enemy destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement option to spawn a higher-tier spell drop at location of death

			EventManager.Instance.Notify(Event.EventTypes.EnemyDeath);
            Destroy(gameObject);
        }
    }
}
