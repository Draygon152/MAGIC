using UnityEngine;

//Written by Liz
public abstract class HealthManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;



    // Initialize current object's health to startingHealth
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Current {gameObject.tag} Health: {currentHealth}");
    }


    public abstract void LoseHealth(int damageAmount);
}