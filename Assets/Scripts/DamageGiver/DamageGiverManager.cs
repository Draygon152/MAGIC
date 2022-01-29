using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiverManager : MonoBehaviour
{
    // When a game object (either a player, enemy, or physical object) causes damage to a different
    // object, it will call its damage function respectively.
    public void DamagePlayer(PlayerHealthManager player, int amountOfDamage)
    {
        player.LoseHealth(amountOfDamage);
    }

    public void DamageEnemy(EnemyHealthManager enemy, int amountOfDamage)
    {
        enemy.LoseHealth(amountOfDamage);
    }

    public void DamageObject(ObjectHealthManager obj, int amountOfDamage)
    {
        obj.LoseHealth(amountOfDamage);
    }

    
}
