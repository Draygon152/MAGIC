// Written by Liz
// Modified by Kevin Chao

using UnityEngine;

public class DamageGiverManager : MonoBehaviour
{
    public void DamageTarget(HealthManager target, int amountOfDamage)
    {
        target.LoseHealth(amountOfDamage);
    }
}