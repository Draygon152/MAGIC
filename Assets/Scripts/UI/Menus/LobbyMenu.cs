// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : Menu<LobbyMenu>
{
    [SerializeField] private Toggle player1Ready;
    [SerializeField] private Toggle player2Ready;
    [SerializeField] private ElementSelector p1ElementSelector; // UI elements for players to select Elemental Affinity
    [SerializeField] private ElementSelector p2ElementSelector;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private CountdownTimer timer; // Timer, provides countdown, game starts when countdown reaches 0

    private bool player1IsReady;
    private bool player2IsReady;

    private PlayerData p1Data;
    private PlayerData p2Data;



    private void Start()
    {
        p1Data = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
        p2Data = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_2);

        player1IsReady = false;
        player2IsReady = false;
    }


    // For use by GUI buttons
    public void Player1SelectedElement()
    {
        player1Ready.interactable = true;

        // Store element data in PlayerData1
        p1Data.SetElement(p1ElementSelector.GetSelectedElement());
    }


    // For use by GUI buttons
    public void Player2SelectedElement()
    {
        player2Ready.interactable = true;

        // Store element data in PlayerData2
        p2Data.SetElement(p2ElementSelector.GetSelectedElement());
    }


    // For use by GUI toggle
    public void Player1TogglePressed()
    {
        // If the toggle has just been activated
        if (player1Ready.isOn == true)
        {
            p1ElementSelector.DisableAllElementButtons();

            // Set Player1 readystate to true
            player1IsReady = true;

            // If Player2 is also ready, start countdown
            if (player2IsReady)
            {
                timer.BeginCountDown();
                mainMenuButton.interactable = false;
            }
        }

        // If the toggle has just been deactivated
        else if (player1Ready.isOn == false)
        {
            p1ElementSelector.EnableAllElementButtons();

            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
            {
                timer.StopCountDown();
                mainMenuButton.interactable = true;
            }

            // Set Player1's readystate to false
            player1IsReady = false;
        }
    }


    // For use by GUI toggle
    public void Player2TogglePressed()
    {
        // If the toggle has just been activated
        if (player2Ready.isOn == true)
        {
            p2ElementSelector.DisableAllElementButtons();

            // Set Player2 readystate to true
            player2IsReady = true;

            // If Player1 is also ready, start countdown
            if (player1IsReady)
            {
                timer.BeginCountDown();
                mainMenuButton.interactable = false;
            }
        }

        // If the toggle has just been deactivated
        else if (player2Ready.isOn == false)
        {
            p2ElementSelector.EnableAllElementButtons();

            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
            {
                timer.StopCountDown();
                mainMenuButton.interactable = true;
            }

            // Set Player2's readystate to false
            player2IsReady = false;
        }
    }


    // For use by GUI button
    public void ReturnToMainMenu()
    {
        Close();
    }
}