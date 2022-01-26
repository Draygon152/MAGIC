using System.Collections;
using UnityEngine;
using System;

public class EventManagerTester : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Tester Awake");
    }


    private void OnEnable()
    {
        Debug.Log("Tester Enabled");
    }


    private void OnDisable()
    {
        Debug.Log("Tester Disabled");
        EventManager.Instance.Unsubscribe(Event.EventTypes.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Unsubscribe(Event.EventTypes.PlayerDeath, PlayerDeathListener);
    }


    private void Start()
    {
        Debug.Log("Tester Started");

        EventManager.Instance.Subscribe(Event.EventTypes.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Subscribe(Event.EventTypes.PlayerDeath, PlayerDeathListener);
        StartCoroutine(TestStart());
    }


    IEnumerator TestStart()
    {
        WaitForSeconds waitTime = new WaitForSeconds(2);
        while (true)
        {
            yield return waitTime;
            EventManager.Instance.Notify(Event.EventTypes.EnemyDeath);

            yield return waitTime;
            EventManager.Instance.Notify(Event.EventTypes.PlayerDeath);
        }
    }


    void EnemyDeathListener()
    {
        Debug.Log("Enemy Dead");
    }


    void PlayerDeathListener()
    {
        Debug.Log("Player Dead");
    }
}
