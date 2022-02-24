// Written by Kevin Chao

using System.Collections;
using UnityEngine;

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
        EventManager.Instance.Unsubscribe(EventTypes.Events.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Unsubscribe(EventTypes.Events.PlayerDeath, PlayerDeathListener);
    }


    private void Start()
    {
        Debug.Log("Tester Started");

        // Subscribe listening functions to testing events
        EventManager.Instance.Subscribe(EventTypes.Events.EnemyDeath, EnemyDeathListener);
        EventManager.Instance.Subscribe(EventTypes.Events.PlayerDeath, PlayerDeathListener);

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
            EventManager.Instance.Notify(EventTypes.Events.EnemyDeath);

            yield return waitTime;
            EventManager.Instance.Notify(EventTypes.Events.PlayerDeath);
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
