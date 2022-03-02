// Written by Liz
// Modified by Kevin Chao and Angel

using UnityEngine;

public class ObjectHealthManager : HealthManager
{
    [SerializeField] private SpellItem spellItem;

    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Health of {gameObject.name} after damage: {currentHealth}");

        // If health becomes 0 or less, object destroyed
        if (currentHealth <= 0)
        {
            // TODO: Implement code to spawn in a spell drop at current object location

            gameObject.SetActive(false);
            spawnSpellItem();
        }
    }

    private void spawnSpellItem()
    {
        if(gameObject.tag == "Crate")
        {
            Instantiate(spellItem, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}