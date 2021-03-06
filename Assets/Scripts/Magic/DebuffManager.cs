// Written by Angel
// Modified by Kevin Chao

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DebuffManager : MonoBehaviour
{
    private int storedDamage;
    private float storedSpeed;
    private float storedAccel;
    private NavMeshAgent navMesh;



    private void Start()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        storedDamage = this.GetComponent<MeleeDamageGiver>().CurrentDamage();
        storedSpeed = this.GetComponent<EnemyBehaviorBase>().ReturnSpeed();
    }


    public void SpeedChange(float time, float change)
    {
        if (change == 0)
        {
            this.GetComponent<NavMeshAgent>().isStopped = true;
        }

        else
        {
            this.GetComponent<EnemyBehaviorBase>().ChangeSpeed(storedSpeed * change);
        }

        StartCoroutine(EffectDuration(time));
    }


    public void DamageChange(float time)
    {
        this.GetComponent<MeleeDamageGiver>().ChangeDamage(0);
        StartCoroutine(EffectDuration(time));
    }


    private IEnumerator EffectDuration(float time)
    {
        yield return new WaitForSeconds(time);
        RevertDamage();
        RevertSpeed();
    }


    public void SustainedDamage(float time, float damage)
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
        }
    }


    private void RevertSpeed()
    {
        this.GetComponent<EnemyBehaviorBase>().ChangeSpeed(storedSpeed);
        this.GetComponent<NavMeshAgent>().isStopped = false;
    }


    private void RevertDamage()
    {
        this.GetComponent<MeleeDamageGiver>().ChangeDamage(storedDamage);
    }
}