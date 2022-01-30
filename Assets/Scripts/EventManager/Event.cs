// Sealed class to hold EventTypes enum
public sealed class Event
{
    public enum EventTypes
    {
        GameStart,
        GamePaused,
        GameUnpaused,
        PlayerDeath,
        EnemyDeath,
        PlayerVictory,
        PlayerDefeat
    }
}