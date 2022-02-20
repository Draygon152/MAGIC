// Written by Kevin Chao
// Modified by Angel

using System;
using TMPro;
using UnityEngine;

public class HUD : Menu<HUD>
{

    [SerializeField] private HealthBar Player1HealthBar;
    [SerializeField] private HealthBar Player2HealthBar;
    [SerializeField] private SelectedSpellUI Player1SpellUI;
    [SerializeField] private SelectedSpellUI Player2SpellUI;
    [SerializeField] private TextMeshProUGUI enemyCounter;




    public void SetP1CurHealth(int newHealth)
    {
        Player1HealthBar.SetCurHealth(newHealth);
    }


    public void SetP1MaxHealth(int maxHealth)
    {
        Player1HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP2CurHealth(int newHealth)
    {
        Player2HealthBar.SetCurHealth(newHealth);
    }


    public void SetP2MaxHealth(int maxHealth)
    {
        Player2HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP1SpellCaster(MagicCasting caster)
    {
        Player1SpellUI.InitializeSpellUI(caster);
    }


    public void SetP2SpellCaster(MagicCasting caster)
    {
        Player2SpellUI.InitializeSpellUI(caster);
    }

    public void SetEnemyCouter(int enemiesRemaining)
    {
        enemyCounter.text = "Enemies Remaining: " + Convert.ToString(enemiesRemaining);
    }
}