// Written by Liz

using UnityEngine;

public abstract class HealthManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;



    // Initialize current object's health to startingHealth
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Current {gameObject.name} Health: {currentHealth}");
    }

    virtual public void GainHealth(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public abstract void LoseHealth(int damageAmount);


    //The following functions are for checking if the health of an object is
    //above/below a certain threshold

    public bool HealthAboveAmountThreshold(int threshold)
    {
        return currentHealth > threshold;
    }

    public bool HealthBelowAmountThreshold(int threshold)
    {
        return currentHealth < threshold;
    }

    public bool HealthAbovePercentageThreshold(float threshold)
    {
        float percentageOfMaxHealth = (float)currentHealth / maxHealth;

        return percentageOfMaxHealth > threshold;
    }

    public bool HealthBelowPercentageThreshold(float threshold)
    {
        float percentageOfMaxHealth = (float)currentHealth / maxHealth;

        return percentageOfMaxHealth < threshold;
    }
}