// Written by Lizbeth
// Modified by Kevin Chao and Angel Rubio

using UnityEngine;
using System;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] protected PlayerHealthBar healthBar;
    [SerializeField] protected int startingHealth; // Health the player starts at after resurrection, not yet implemented



    protected override void Start()
    {
        base.Start();

        healthBar.InitializeHealthBar(maxHealth);
    }


    public override void GainHealth(int healAmount)
    {
        base.GainHealth(healAmount);

        healthBar.SetHealth(currentHealth);
    }


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        // If health becomes 0 or less, player dies
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            healthBar.SetHealth(currentHealth);

            // If the player runs out of hp, disable the player instead of destroying, allows for resurrection
            gameObject.SetActive(false);

            EventManager.Instance.Notify(EventTypes.Events.PlayerDeath);
        }

        // If health is not yet empty, just update HealthBar
        else
        {
            healthBar.SetHealth(currentHealth);
        }
    }


    public int GetMaxHealth()
    {
        return maxHealth;
    }
    

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}