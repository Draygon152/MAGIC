// Written by Liz
// Modified by Kevin Chao

using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] protected int startingHealth; // Health the player starts at after resurrection, not yet implemented

    public delegate void UpdateHealthBar(int newCurrentHealth);
    public delegate void UpdateHealthBarMax(int newMaxHealth);

    public UpdateHealthBar setHealthBarValue;  // Contains pointer to function responsible for setting HealthBar's current value
    public UpdateHealthBarMax setHealthBarMax; // Contains pointer to function responsible for setting HealthBar max



    protected override void Start()
    {
        base.Start();

        InitializeHealthBar();
    }


    private void InitializeHealthBar()
    {
        setHealthBarMax(maxHealth);
        setHealthBarValue(currentHealth);
    }


    public void GainHealth(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        setHealthBarValue(currentHealth);
    }


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Health of {gameObject.name} after damage: {currentHealth}");

        // If health becomes 0 or less, player dies
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            setHealthBarValue(currentHealth);

            // If the player runs out of hp, disable the player instead of destroying, allows for resurrection
            gameObject.SetActive(false);

            EventManager.Instance.Notify(Event.EventTypes.PlayerDeath);
        }

        // If health is not yet empty, just update HealthBar
        else
            setHealthBarValue(currentHealth);
    }
}