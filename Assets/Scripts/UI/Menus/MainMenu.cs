// Written by Kevin Chao

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void SingleplayerPressed()
    {
        SingleplayerLobbyMenu.Open();
    }


    public void MultiplayerPressed()
    {
        MultiplayerLobbyMenu.Open();
    }


    public void OptionsPressed()
    {
        OptionsMenu.Open();
    }


    public void ExitGame()
    {
        Debug.Log("Exiting Game");

        Application.Quit();
    }
}