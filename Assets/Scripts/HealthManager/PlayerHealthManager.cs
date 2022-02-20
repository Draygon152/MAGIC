// Written by Liz
// Modified by Kevin Chao & Angel Rubio

using UnityEngine;
using System;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] protected int startingHealth; // Health the player starts at after resurrection, not yet implemented

    private Action<int> setHealthBarValue; // Contains pointer to function responsible for setting HealthBar's current value
    private Action<int> setHealthBarMax;   // Contains pointer to function responsible for setting HealthBar max



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


    public override void GainHealth(int healAmount)
    {
        base.GainHealth(healAmount);

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

            EventManager.Instance.Notify(EventTypes.Events.PlayerDeath);
        }

        // If health is not yet empty, just update HealthBar
        else
            setHealthBarValue(currentHealth);
    }


    public void SetHealthBarDelegates(Action<int> setHBValue, Action<int> setHBValueMax)
    {
        setHealthBarValue = setHBValue;
        setHealthBarMax = setHBValueMax;
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