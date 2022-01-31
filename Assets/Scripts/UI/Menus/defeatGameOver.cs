using UnityEngine;

public class defeatGameOver : Menu<defeatGameOver>
{
    public void ReturnToMainMenuPressed()
    {
        EventManager.Instance.Notify(Event.EventTypes.ResetGame);
        MainMenu.Open();
        //Add code to reset GameManager state back to Start?
    }
}
