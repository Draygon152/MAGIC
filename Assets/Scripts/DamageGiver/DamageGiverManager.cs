using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiverManager : MonoBehaviour
{
    // When a game object (either a player, enemy, or physical object) causes damage to a different
    // object, it will call its damage function respectively.
    public int amountOfDamage = 1;
    public void DamagePlayer(PlayerHealthManager player)
    {
        player.LoseHealth(amountOfDamage);
    }

    public void DamageEnemy(EnemyHealthManager enemy, int Dmg = 1)
    {

        enemy.LoseHealth(Dmg);
    }

    public void DamageObject(ObjectHealthManager obj)
    {
        obj.LoseHealth(amountOfDamage);
    }

    
}
