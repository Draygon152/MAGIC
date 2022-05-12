// Written by Kevin Chao
// Modified by Marc Hagoriles

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void SingleplayerPressed()
    {
        //load lobby scene
        GameSceneManager.Instance.LoadScene(GameSceneManager.Scenes.LOBBY_SCENE, GameSceneManager.NetworkSceneMode.LOCAL);

        SingleplayerLobbyMenu.Open();
    }


    public void MultiplayerPressed()
    {
        //load lobby scene
        GameSceneManager.Instance.LoadScene(GameSceneManager.Scenes.LOBBY_SCENE, GameSceneManager.NetworkSceneMode.LOCAL);

        MultiplayerLobbyMenu.Open();
    }

    public void HowToPlayPressed()
    {
        HowToPlayMenu.Open();
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