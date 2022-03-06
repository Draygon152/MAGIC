// Written by Kevin Chao
// Modified by Angel and Lawson

using System;
using TMPro;
using UnityEngine;

public class HUD : Menu<HUD>
{

    [SerializeField] private HealthBar player1HealthBar;
    [SerializeField] private HealthBar player2HealthBar;
    [SerializeField] private SelectedSpellUI player1SpellUI;
    [SerializeField] private SelectedSpellUI player2SpellUI;
    [SerializeField] private TextMeshProUGUI enemyCounter;




    public void SetP1CurHealth(int newHealth)
    {
        player1HealthBar.SetCurHealth(newHealth);
    }


    public void SetP1MaxHealth(int maxHealth)
    {
        player1HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP2CurHealth(int newHealth)
    {
        player2HealthBar.SetCurHealth(newHealth);
    }


    public void SetP2MaxHealth(int maxHealth)
    {
        player2HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP1SpellCaster(MagicCasting caster)
    {
        player1SpellUI.InitializeSpellUI(caster);
    }


    public void SetP2SpellCaster(MagicCasting caster)
    {
        player2SpellUI.InitializeSpellUI(caster);
    }

    public void SetP1MaxCooldown(float newCooldown)
    {
        player1SpellUI.changeSpellCooldown(newCooldown);
    }

    public void SetP2MaxCooldown(float newCooldown)
    {
        player2SpellUI.changeSpellCooldown(newCooldown);
    }


    public void SetEnemyCouter(int enemiesRemaining)
    {
        enemyCounter.text = "Enemies Remaining: " + Convert.ToString(enemiesRemaining);
    }

    public float ReturnCooldown(int playerNumber)
    {
        if(playerNumber == 0)
        {
            return player1SpellUI.CheckSpellCooldownValue();
        }
        else
        {
            return player2SpellUI.CheckSpellCooldownValue();
        }
    }

}