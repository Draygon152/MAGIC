// Written by Lawson McCoy
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;



    // A setup function to set the max health of the enemy
    // and to start the enemy with their max health
    public void InitializeHealthBar(int maxHealth)
    {
        // Set health bar maximum
        healthBar.maxValue = maxHealth;

        // Set current health value to be max health
        healthBar.value = maxHealth;
    }


    // A function for setting the health bar to a new value
    public void UpdateHealth(int newHealth)
    {
        healthBar.value = newHealth;
    }
}