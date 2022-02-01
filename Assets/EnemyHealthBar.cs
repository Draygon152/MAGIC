using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBarValue;

    //A setup fucntion to set the max health of the enemy
    //and to start the enemy with their max health
    public void InitializeHealthBar(int maxHealth)
    {
        //set max health
        healthBarValue.maxValue = maxHealth;

        //set health to be max health
        healthBarValue.value = maxHealth;
    }

    //A function for setting the health bar to a new value
    public void UpdateHealth(int newHealth)
    {
        healthBarValue.value = newHealth;
    }
}

