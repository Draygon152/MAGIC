// Written by Kevin Chao

using UnityEngine;

public class PauseMenu : Menu<PauseMenu>
{
    public void OnOptionsPress()
    {
        OptionsMenu.Open();
    }


    public void OnMainMenuPress()
    {
        EventManager.Instance.Notify(EventTypes.Events.ResetGame);
        Close();
        MainMenu.Open();
    }
}