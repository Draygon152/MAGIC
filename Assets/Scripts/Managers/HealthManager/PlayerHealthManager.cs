// Written by Liz
// Modified by Kevin Chao and Angel Rubio

using UnityEngine;
using System;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] protected int startingHealth; // Health the player starts at after resurrection, not yet implemented

    private Action<int, int> setHealthBarValue; // Contains pointer to function responsible for setting HealthBar's current value
    private Action<int, int> setHealthBarMax;   // Contains pointer to function responsible for setting HealthBar max
    private int playerNumber;



    protected override void Start()
    {
        base.Start();

        InitializeHealthBar();
    }


    private void InitializeHealthBar()
    {
        setHealthBarMax(playerNumber, maxHealth);
        setHealthBarValue(playerNumber, currentHealth);
    }


    public override void GainHealth(int healAmount)
    {
        base.GainHealth(healAmount);

        setHealthBarValue(playerNumber, currentHealth);
    }


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        // If health becomes 0 or less, player dies
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            setHealthBarValue(playerNumber, currentHealth);

            // If the player runs out of hp, disable the player instead of destroying, allows for resurrection
            gameObject.SetActive(false);

            EventManager.Instance.Notify(EventTypes.Events.PlayerDeath);
        }

        // If health is not yet empty, just update HealthBar
        else
            setHealthBarValue(playerNumber, currentHealth);
    }


    public void SetHealthBarDelegates(int playerNum, Action<int, int> setHBValue, Action<int, int> setHBValueMax)
    {
        playerNumber = playerNum;
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