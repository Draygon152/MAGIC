using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    /*
    // Currently, LoseHealth() deletes the player since it dies when reached to 0.
    // However, deleting a player object will cause an error because of the camera looking for it.

    // LoseHealth() substracts Player's health according to the number of damage.
    public override void LoseHealth(int numDamage)
    {
        currentHealth -= numDamage;
        if (currentHealth <= 0)
        {
            //Trigger an event when Player reaches 0 (for example, a gameover screens)
        }
    }
    */
}
