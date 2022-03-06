// Written by Kevin Chao
// Modified by Angel and Lawson

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : Menu<HUD>
{
    // Player 1 = index 0, Player 2 = index 1, etc.
    [SerializeField] private HealthBar[] playerHealthBars;
    [SerializeField] private SelectedSpellUI[] playerSpellUIs;

    [SerializeField] private TextMeshProUGUI enemyCounter;

    

    public void SetPlayerCurHealth(int playerNum, int newHealth)
    {
        playerHealthBars[playerNum].SetCurHealth(newHealth);
    }


    public void SetPlayerMaxHealth(int playerNum, int newHealth)
    {
        playerHealthBars[playerNum].SetMaxHealth(newHealth);
    }


    public void SetPlayerSpellCaster(int playerNum, MagicCasting caster)
    {
        playerSpellUIs[playerNum].InitializeSpellUI(caster);
    }


    public void SetPlayerMaxCooldown(int playerNum, float newCooldown)
    {
        playerSpellUIs[playerNum].ChangeSpellCooldown(newCooldown);
    }


    public void SetEnemyCounter(int enemiesRemaining)
    {
        enemyCounter.text = "Enemies Remaining: " + Convert.ToString(enemiesRemaining);
    }
}