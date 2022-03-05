// Written by Lizbeth
// Modified by Kevin Chao and Angel Rubio

using System.Collections;
using UnityEngine;

// Request to change this function in a different and appropriate name
public class CollisionDamageGiver : DamageGiver
{
    [SerializeField] private int damageDealt; // Attack power
    [SerializeField] private float damageOverTime; // Attack player in X seconds overtime

    public float GetDamageOverTime()
    {
        return damageOverTime;
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