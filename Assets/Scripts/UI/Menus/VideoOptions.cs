// Written by Marc Hagoriles
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOptions : Menu<VideoOptions>
{
    Resolution[] resolutionOptions;
    [SerializeField] private Dropdown resolutionDropdown;
   

    private void Start()
    {
        //Get an array of all resolutions available to the player.
        resolutionOptions = Screen.resolutions;

        //At first, clear the dropdown options because we will set this to the available options to the player.
        resolutionDropdown.ClearOptions();

        //Initalize a dynamic list that should hold our resolution options.
        List<string> options = new List<string>();

        //Before we start looping through, get our current resolution index.
        int currResIndex = 0;

        //For each available resolution option, store it in options list
        for (int i = 0; i < resolutionOptions.Length; i++)
        {
            string option = resolutionOptions[i].width + "x" + resolutionOptions[i].height + "@" + resolutionOptions[i].refreshRate + "Hz";
            options.Add(option);

            //If our current resolution matches one of the available resolution options, make that our default resolution shown in the dropdown menu.
            if (resolutionOptions[i].width == Screen.width && resolutionOptions[i].height == Screen.height)
            {
                currResIndex = i;
            }
        }

        //Add the stored options in the dropdown menu.
        resolutionDropdown.AddOptions(options);
        //Default resolution shown in the dropdown menu should be our current resolution.
        resolutionDropdown.value = currResIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutionOptions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ReturnPressed()
    {
        Close();
    }
}