// Written by Kevin Chao
// Modified by Angel and Lawson

using TMPro;
using UnityEngine;

public class HUD : Menu<HUD>
{
    // Player 1 = index 0, Player 2 = index 1, etc.
    [SerializeField] private HealthBar[] playerHealthBars;
    [SerializeField] private SelectedSpellUI[] playerSpellUIs;

    [SerializeField] private TextMeshProUGUI enemyCounter;



    public void EnablePlayerHUDElements(int playerNum)
    {
        playerHealthBars[playerNum].gameObject.SetActive(true);
        playerSpellUIs[playerNum].gameObject.SetActive(true);
    }


    public void DisablePlayerHUDElements(int playerNum)
    {
        playerHealthBars[playerNum].gameObject.SetActive(false);
        playerSpellUIs[playerNum].gameObject.SetActive(false);
    }


    public void SetPlayerCurHealth(int playerNum, int newHealth)
    {
        playerHealthBars[playerNum].SetHealth(newHealth);
    }


    public void SetPlayerMaxHealth(int playerNum, int newHealth)
    {
        playerHealthBars[playerNum].SetHealth(newHealth);
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
        enemyCounter.text = $"Enemies Remaining: {enemiesRemaining}";
    }


    public float ReturnCooldown(int playerNumber)
    {
        return playerSpellUIs[playerNumber].CheckSpellCooldownValue();
    }
}