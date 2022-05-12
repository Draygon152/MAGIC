// Created by Marc Hagoriles
using UnityEngine;
public class HowToPlayMenu : Menu<HowToPlayMenu>
{
    // When the Return button is pressed, close this menu.
    public void ReturnToMainMenuPressed()
    {
        Close();
    }
}
