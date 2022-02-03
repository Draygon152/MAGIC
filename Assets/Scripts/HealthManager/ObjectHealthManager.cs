// Written by Liz
// Modified by Kevin Chao

using UnityEngine;

public class ObjectHealthManager : HealthManager
{
    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Health of {gameObject.name} after damage: {currentHealth}");

        // If health becomes 0 or less, object destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement code to spawn in a spell drop at current object location

            Destroy(gameObject);
        }
    }
}
