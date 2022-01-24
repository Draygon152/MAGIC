using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Singleton
public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action> subscriberDict;
    private static EventManager eventManager;

    public static Action GamePausedCallback;
    public static Action GameUnpausedCallback;
    public static Action PlayerReadyCallback;


    public static EventManager Instance
    {
        get;
        private set;
    }


    private void Awake()
    {
        Debug.Log("EventManager Awake");

        if (Instance != null)
            Destroy(gameObject);

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            subscriberDict = new Dictionary<string, Action>();
        }
    }


    private void OnDestroy()
    {
        Debug.Log("EventManager Destroyed");

        Instance = null;
    }
}
