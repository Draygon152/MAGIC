// Written by Marc Hagoriles
// Modified by Kevin Chao

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOptions : Menu<VideoOptions>
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    private Resolution[] resolutionOptions;
    private List<string> dropdownOptions;



    private void Start()
    {
        // Initalize a dynamic list that should hold our resolution options.
        dropdownOptions = new List<string>();

        // Get an array of all resolutions available to the player.
        resolutionOptions = Screen.resolutions;

        // At first, clear the dropdown options because we will set this to the available options to the player.
        resolutionDropdown.ClearOptions();

        fullscreenToggle.isOn = Screen.fullScreen;

        // Before we start looping through, get our current resolution index.
        int currResIndex = 0;

        string option;
        // For each available resolution option, store it in options list
        for (int i = 0; i < resolutionOptions.Length; i++)
        {
            option = resolutionOptions[i].width + "x" + resolutionOptions[i].height + "@" + resolutionOptions[i].refreshRate + "Hz";
            dropdownOptions.Add(option);

            // If our current resolution matches one of the available resolution options, make that our default resolution shown in the dropdown menu.
            if (resolutionOptions[i].width == Screen.width && resolutionOptions[i].height == Screen.height)
            {
                currResIndex = i;
            }
        }

        // Add the stored options in the dropdown menu.
        resolutionDropdown.AddOptions(dropdownOptions);

        // Default resolution shown in the dropdown menu should be our current resolution.
        resolutionDropdown.value = currResIndex;
        resolutionDropdown.RefreshShownValue();
    }


    // Set the resolution using the dropdown menu.
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionOptions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    // Set the game to fullscreen mode.
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    // When the return button is pressed, close this menu.
    public void ReturnPressed()
    {
        Close();
    }
}