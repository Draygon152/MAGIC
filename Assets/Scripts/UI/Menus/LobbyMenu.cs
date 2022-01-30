using UnityEngine;
using UnityEngine.UI;
using System;

public class LobbyMenu : Menu<LobbyMenu>
{
    [SerializeField] private Toggle Player1Ready;
    [SerializeField] private Toggle Player2Ready;
    [SerializeField] private ElementSelector P1ElementSelector; // UI elements for players to select Elemental Affinity
    [SerializeField] private ElementSelector P2ElementSelector;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private CountdownTimer timer; // Timer, provides countdown, game starts when countdown reaches 0

    private bool player1IsReady;
    private bool player2IsReady;

    private void Start()
    {
        player1IsReady = false;
        player2IsReady = false;
    }


    public void Player1SelectedElement()
    {
        Player1Ready.interactable = true;
    }


    public void Player2SelectedElement()
    {
        Player2Ready.interactable = true;
    }


    public void Player1TogglePressed()
    {
        // If the toggle has just been activated
        if (Player1Ready.isOn == true)
        {
            P1ElementSelector.DisableAllElementButtons();

            // Set Player1 readystate to true
            player1IsReady = true;

            // If Player2 is also ready, start countdown
            if (player2IsReady)
            {
                timer.BeginCountDown(P1ElementSelector.SelectedElement, P2ElementSelector.SelectedElement);
                MainMenuButton.interactable = false;
            }
        }

        // If the toggle has just been deactivated
        else if (Player1Ready.isOn == false)
        {
            P1ElementSelector.EnableAllElementButtons();

            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
            {
                timer.StopCountDown();
                MainMenuButton.interactable = true;
            }

            // Set Player1's readystate to false
            player1IsReady = false;
        }
    }


    public void Player2TogglePressed()
    {
        // If the toggle has just been activated
        if (Player2Ready.isOn == true)
        {
            P2ElementSelector.DisableAllElementButtons();

            // Set Player2 readystate to true
            player2IsReady = true;

            // If Player1 is also ready, start countdown
            if (player1IsReady)
            {
                timer.BeginCountDown(P1ElementSelector.SelectedElement, P2ElementSelector.SelectedElement) ;
                MainMenuButton.interactable = false;
            }
        }

        // If the toggle has just been deactivated
        else if (Player2Ready.isOn == false)
        {
            P2ElementSelector.EnableAllElementButtons();

            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
            {
                timer.StopCountDown();
                MainMenuButton.interactable = true;
            }

            // Set Player2's readystate to false
            player2IsReady = false;
        }
    }


    public void ReturnToMainMenu()
    {
        Close();
    }
}