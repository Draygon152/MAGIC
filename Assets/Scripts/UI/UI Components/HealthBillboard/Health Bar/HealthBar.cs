// Written by Kevin Chao and Lawson McCoy

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to slider used in health bar
    [SerializeField] protected Slider healthSlider;



    // A setup function to set the max health of the enemy
    // and to start the enemy with their max health
    public void InitializeHealthBar(int maxHealth)
    {
        // Set max value of healthSlider
        healthSlider.maxValue = maxHealth;

        // Set current value of healthSlider to max
        healthSlider.value = maxHealth;
    }


    public void SetHealth(int newHealth)
    {
        healthSlider.value = newHealth;
    }
}