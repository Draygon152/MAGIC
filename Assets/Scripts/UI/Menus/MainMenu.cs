// Written by Kevin Chao

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void SingleplayerPressed()
    {
        SingleplayerLobbyMenu.Open();
        MinimapCameraSystem.Instance.OpenSinglePlayerMinimap();
    }


    public void MultiplayerPressed()
    {
        MultiplayerLobbyMenu.Open();
        MinimapCameraSystem.Instance.OpenMultiplayerMinimap();
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