// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LobbyMenu : Menu<LobbyMenu>
{
    [SerializeField] private Toggle player1Ready;
    [SerializeField] private Toggle player2Ready;
    [SerializeField] private ElementSelector p1ElementSelector; // UI elements for players to select Elemental Affinity
    [SerializeField] private ElementSelector p2ElementSelector;
    [SerializeField] private Dropdown p1InputSelector;
    [SerializeField] private Dropdown p2InputSelector;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private CountdownTimer timer; // Timer, provides countdown, game starts when countdown reaches 0

    private bool player1IsReady;
    private bool player2IsReady;

    private PlayerData p1Data;
    private PlayerData p2Data;

    // Contains assigned device names and references to the actual InputDevice objects
    private Dictionary<string, InputDevice> availableDevices;



    private void Start()
    {
        p1Data = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
        p2Data = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_2);

        player1IsReady = false;
        player2IsReady = false;

        availableDevices = FilterInputDevices();

        p1InputSelector.AddOptions(new List<string> { "Select Input Device" });
        p2InputSelector.AddOptions(new List<string> { "Select Input Device" });
        p1InputSelector.AddOptions(new List<string>(availableDevices.Keys));
        p2InputSelector.AddOptions(new List<string>(availableDevices.Keys));
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


    public void Player1InputDeviceSelected()
    {
        string selectedOption = p1InputSelector.options[p1InputSelector.value].text;

        if (selectedOption == "Select Input Device")
        {
            p1Data.pairedDevice = null;
        }

        else
        {
            p1Data.pairedDevice = availableDevices[selectedOption];
        }

        Debug.Log(p1Data.pairedDevice);
    }


    public void Player2InputDeviceSelected()
    {
        string selectedOption = p2InputSelector.options[p2InputSelector.value].text;

        if (selectedOption == "Select Input Device")
        {
            p2Data.pairedDevice = null;
        }

        else
        {
            p2Data.pairedDevice = availableDevices[selectedOption];
        }

        Debug.Log(p2Data.pairedDevice);
    }


    // For use by GUI button
    public void ReturnToMainMenu()
    {
        Close();
    }


    private Dictionary<string, InputDevice> FilterInputDevices()
    {
        Dictionary<string, InputDevice> output = new Dictionary<string, InputDevice>();
        int gamepadCounter = 0;
        int keyboardCounter = 0;

        foreach (InputDevice device in InputSystem.devices)
        {
            if (device is Gamepad)
            {
                output.Add($"Gamepad {++gamepadCounter}", device);
            }
            
            else if (device is Keyboard)
            {
                output.Add($"Keyboard {++keyboardCounter}", device);
            }
        }

        return output;
    }
}