using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int startingHealth = 5;
    public int maxHealth = 10;
    protected int currentHealth;

    // Start() initialize the current object's health according to startingHealth.
    // Having a startingHealth is necessary in case a player gets revived.
    void Start()
    {
        currentHealth = startingHealth;
        string nameTag = gameObject.tag;
        Debug.Log("Current " + nameTag + " Health: " + currentHealth.ToString());
    }

    public void GainHealth(int numHeal)
    {
        currentHealth += numHeal;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // LoseHealth() substracts object's health according to the number of damage.
    public virtual void LoseHealth(int numDamage)
    {
        currentHealth -= numDamage;
        string nameTag = gameObject.tag;
        Debug.Log("Current " + nameTag + " Health: " + currentHealth.ToString());
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
