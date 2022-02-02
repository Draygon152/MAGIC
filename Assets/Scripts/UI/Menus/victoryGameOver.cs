// Written by Marc Hagoriles

public class VictoryGameOver : Menu<VictoryGameOver>
{
    public void ReturnToMainMenuPressed()
    {
        EventManager.Instance.Notify(Event.EventTypes.ResetGame);
        MainMenu.Open();
        //Add code to reset GameManager state back to Start?
    }
}