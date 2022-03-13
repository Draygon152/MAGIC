// Written by Kevin Chao

public class PauseMenu : Menu<PauseMenu>
{
    public void OnResumePress()
    {
        EventManager.Instance.Notify(EventTypes.Events.GameUnpaused);
    }


    public void OnOptionsPress()
    {
        OptionsMenu.Open();
    }


    public void OnMainMenuPress()
    {
        EventManager.Instance.Notify(EventTypes.Events.ResetGame);

        if (Instance != null)
        {
            Close();
        }
            
        MainMenu.Open();
    }
}