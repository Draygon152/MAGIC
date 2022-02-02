// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to slider used in health bar
    [SerializeField] private Slider HealthSlider;


    public void SetCurHealth(int health)
    {
        HealthSlider.value = health;
    }


    public void SetMaxHealth(int maxHealth)
    {
        HealthSlider.maxValue = maxHealth;
    }
}