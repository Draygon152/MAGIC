// Written by Kevin Chao

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void SingleplayerPressed()
    {
        SingleplayerLobbyMenu.Open();
        //Opens the Singleplayer Minimap Render, added by Marc Hagoriles
        MinimapCameraSystem.Instance.OpenSinglePlayerMinimap();
    }


    public void MultiplayerPressed()
    {
        MultiplayerLobbyMenu.Open();
        //Opens the Multiplayer Minimap Render, added by Marc Hagoriles
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