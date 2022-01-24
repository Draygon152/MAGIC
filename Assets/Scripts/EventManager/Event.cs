using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all events, Singleton
public abstract class Event
{
    public enum EventTypes
    {
        PlayerReady,
        GamePaused,
        GameUnpaused,
        EnemyDeath,
        PlayerDeath
    }


    public static Event Instance
    {
        get;
        private set;
    }
}