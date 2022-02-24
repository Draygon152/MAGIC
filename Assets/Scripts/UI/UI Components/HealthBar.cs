// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to slider used in health bar
    [SerializeField] private Slider healthSlider;



    public void SetCurHealth(int health)
    {
        healthSlider.value = health;
    }


    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }
}