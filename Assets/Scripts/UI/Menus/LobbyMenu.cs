// Written by Kevin Chao

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public abstract class LobbyMenu<T> : Menu<LobbyMenu<T>>
{
    // Player 1 is stored at index 0, Player 2 stored at index 1, etc.
    [SerializeField] protected Toggle[] playerReadyToggles;
    [SerializeField] protected ElementSelector[] elementSelectors;
    [SerializeField] protected Dropdown[] inputSelectors;

    [SerializeField] protected Button mainMenuButton;
    [SerializeField] protected CountdownTimer timer;

    // Contains assigned device names and references to the actual InputDevice objects
    protected Dictionary<string, InputDevice> availableDevices;

    protected int numPlayers;
    protected bool[] playerReadyStates;

    protected PlayerData[] playerDataList;



    protected virtual void Start()
    {
        availableDevices = FilterInputDevices();

        foreach (Dropdown inputSelector in inputSelectors)
        {
            inputSelector.AddOptions(new List<string> { "Select Input Device" });
            inputSelector.AddOptions(new List<string>(availableDevices.Keys));
        }

        //Set up the player data for the PlayerManager
        PlayerManager.Instance.SetNumberOfPlayers(numPlayers);

        playerDataList = new PlayerData[numPlayers];
        playerReadyStates = new bool[numPlayers];

        for (int playerIndex = 0; playerIndex < numPlayers; playerIndex++)
        {
            playerDataList[playerIndex] = PlayerManager.Instance.GetPlayerData(playerIndex);
        }
    }


    public void PlayerSelectedElement(int playerNumber)
    {
        if (playerNumber < 0 || playerNumber > numPlayers - 1)
            Debug.LogException(new Exception($"PlayerSelectedElement: Invalid player number ({playerNumber})"));

        playerReadyToggles[playerNumber].interactable = true;

        // Store element data in PlayerData1
        playerDataList[playerNumber].ElementalAffinity = elementSelectors[playerNumber].GetSelectedElement();
    }


    public void PlayerReadyPressed(int playerNumber)
    {
        if (playerNumber < 0 || playerNumber > numPlayers - 1)
            Debug.LogException(new Exception($"PlayerTogglePressed: Invalid player number ({playerNumber})"));

        // If the toggle has just been activated
        if (playerReadyToggles[playerNumber].isOn == true)
        {
            elementSelectors[playerNumber].DisableAllElementButtons();

            // Set player readystate to true
            playerReadyStates[playerNumber] = true;
            inputSelectors[playerNumber].interactable = false;

            // If all players are ready, start countdown
            if (AllPlayersReady())
            {
                timer.BeginCountDown();
                mainMenuButton.interactable = false;
            }
        }

        // If the toggle has just been deactivated
        else if (playerReadyToggles[playerNumber].isOn == false)
        {
            elementSelectors[playerNumber].EnableAllElementButtons();
            inputSelectors[playerNumber].interactable = true;

            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
            {
                timer.StopCountDown();
                mainMenuButton.interactable = true;
            }

            // Set Player1's readystate to false
            playerReadyStates[playerNumber] = false;
        }
    }


    public void PlayerSelectedInputDevice(int playerNumber)
    {
        if (playerNumber < 0 || playerNumber > numPlayers - 1)
            Debug.LogException(new Exception($"PlayerSelectedInputDevice: Invalid player number ({playerNumber})"));

        string selectedOption = inputSelectors[playerNumber].options[inputSelectors[playerNumber].value].text;

        if (selectedOption == "Select Input Device")
        {
            playerDataList[playerNumber].pairedDevice = null;
        }

        else
        {
            playerDataList[playerNumber].pairedDevice = availableDevices[selectedOption];
        }

        Debug.Log($"PLAYER {playerNumber + 1} SELECTED DEVICE: {playerDataList[playerNumber].pairedDevice}");
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


    private bool AllPlayersReady()
    {
        foreach (bool playerReady in playerReadyStates)
        {
            if (!playerReady)
                return false;
        }

        return true;
    }
}