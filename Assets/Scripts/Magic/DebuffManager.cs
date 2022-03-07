//Written by Angel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebuffManager : MonoBehaviour
{
    private float storedSpeed;
    private int storedDamage;
    private float storedAccel;
    private NavMeshAgent navMesh;

    private void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        storedDamage = this.GetComponent<CollisionDamageGiver>().CurrentDamage();
        storedSpeed = this.GetComponent<EnemyBehaviorBase>().ReturnSpeed();
    }

    public void speedChange(float time, float change)
    {
        if(change == 0)
        {
            this.GetComponent<NavMeshAgent>().isStopped = true;
        }
        else
        {
            this.GetComponent<EnemyBehaviorBase>().ChangeSpeed(storedSpeed * change);
        }
        StartCoroutine(EffectDuration(time));
    }

    public void damageChange(float time)
    {
        this.GetComponent<CollisionDamageGiver>().ChangeDamage(0);
        StartCoroutine(EffectDuration(time));
    }

    private IEnumerator EffectDuration(float time)
    {
        yield return new WaitForSeconds(time);
        revertDamage();
        revertSpeed();
    }

    public void sustainedDamage(float time, float damage)
    {
        StartCoroutine(TickDamage(time, damage));
    }

    private IEnumerator TickDamage(float time, float damage)
    {
        int damageint = (int)damage;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(time);
            this.GetComponent<EnemyHealthManager>().LoseHealth(damageint);
            print("tick");
        }
    }

    private void revertSpeed()
    {
        this.GetComponent<EnemyBehaviorBase>().ChangeSpeed(storedSpeed);
        this.GetComponent<NavMeshAgent>().isStopped = false;
    }

    private void revertDamage()
    {
        this.GetComponent<CollisionDamageGiver>().ChangeDamage(storedDamage);
    }
}
