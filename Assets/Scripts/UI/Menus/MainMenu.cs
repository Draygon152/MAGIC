// Written by Kevin Chao

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void PlayGamePressed()
    {
        LobbyMenu.Open();
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