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

        // Unsubscribe all listening functions from their events, cleanup
        EventManager.Instance.Unsubscribe(Event.EventTypes.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Unsubscribe(Event.EventTypes.PlayerDeath, PlayerDeathListener);
    }


    private void Start()
    {
        Debug.Log("Tester Started");

        // Subscribe listening functions to testing events
        EventManager.Instance.Subscribe(Event.EventTypes.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Subscribe(Event.EventTypes.PlayerDeath, PlayerDeathListener);

        // Begin test, stored in coroutine
        StartCoroutine(TestStart());
    }


    // Testing coroutine, tells EventManager to notify subscribers of a specific event every 2 seconds
    // The expected output is that every 2 seconds, "Enemy Dead" or "Player Dead" will print to the
    // Debug log, and they alternate.
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


    // Listening function to test EnemyDeath event
    void EnemyDeathListener()
    {
        Debug.Log("Enemy Dead");
    }


    // Listening function to test PlayerDeath event
    void PlayerDeathListener()
    {
        Debug.Log("Player Dead");
    }
}
