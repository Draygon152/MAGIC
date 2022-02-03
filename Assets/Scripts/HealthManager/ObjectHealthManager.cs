using UnityEngine;

//Written by Liz
public class ObjectHealthManager : HealthManager
{
    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Health of {gameObject.tag} after damage: {currentHealth}");

        // If health becomes 0 or less, object destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement code to spawn in a spell drop at current object location

            Destroy(gameObject);
        }
    }
}
