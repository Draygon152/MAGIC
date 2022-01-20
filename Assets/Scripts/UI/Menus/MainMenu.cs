using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void PlayGamePressed()
    {
        // Fill in game start or additional gamemode selection menu code here
    }


    public void OptionsPressed()
    {
        OptionsMenu.Open();
    }


    public void ExitGame()
    {
        Debug.Log("Exiting Game");

        // Uncomment when testing quitting functionality from within
        // Unity's editor, since Application.Quit() does nothing there
        // UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}