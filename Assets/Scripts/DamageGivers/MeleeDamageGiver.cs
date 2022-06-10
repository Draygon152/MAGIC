// Written by Lizbeth
// Modified by Kevin Chao and Angel Rubio

using UnityEngine;

// Request to change this function in a different and appropriate name
public class MeleeDamageGiver : DamageGiver
{
    [SerializeField] private int damageDealt; // Attack power
    [SerializeField] private float attackCooldown = 5.0f; // Cooldown between attacks



    public float GetAttackCooldown()
    {
        return attackCooldown;
    }


    public int CurrentDamage()
    {
        return damageDealt;
    }


    public void ChangeDamage(int newdamage)
    {
        damageDealt = newdamage;
    }
}