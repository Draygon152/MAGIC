// Written by Kevin Chao

#undef DEBUG

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

        // Uncomment when testing quitting functionality from within
        // Unity's editor, since Application.Quit() does nothing there
#if (DEBUG)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}