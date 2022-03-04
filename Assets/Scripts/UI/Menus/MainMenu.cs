// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

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