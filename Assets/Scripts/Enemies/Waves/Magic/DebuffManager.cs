using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    private float storedSpeed;
    private float storedDamage;

    private void Awake()
    {
        storedDamage = this.GetComponent<CollisionDamageGiver>().currentDamage();
    }

    private void speedChange()
    {

    }

    private void damageChange()
    {

    }

    private void sustainedDamage()
    {

    }

    private void revertSpeed()
    {

    }

    private void revertDamage()
    {

    }
}
