// Written by Kevin Chao
// Modified by Angel and Lawson

using TMPro;
using UnityEngine;

public class HUD : Menu<HUD>
{
    [SerializeField] private TextMeshProUGUI enemyCounter;


    public void SetEnemyCounter(int enemiesRemaining)
    {
        enemyCounter.text = $"Enemies Remaining: {enemiesRemaining}";
    }
}