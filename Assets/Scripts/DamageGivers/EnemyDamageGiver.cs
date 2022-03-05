// Written by Liz
// Modified by Kevin Chao
using System.Collections;
using UnityEngine;

// Request to change this function in a different and appropriate name
public class EnemyDamageGiver : DamageGiver
{
    [SerializeField] private int damageDealt; // Attack power
    [SerializeField] private float damageOverTime; // Attack player in X seconds overtime

    public int GetDamageDealt()
    {
        return damageDealt;
    }

    public float GetDamageOverTime()
    {
        return damageOverTime;
    }
}