// Written by Kevin Chao

using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    [SerializeField] private bool debug;


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

        if (debug)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}