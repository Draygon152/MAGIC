// Written by Marc Hagoriles
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// Implementation using PlayerPrefs is due to a lack of time, future builds
// would set up a system of loading/saving this information without the
// use of hardcoded strings
public class SoundOptions : Menu<SoundOptions>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;



    private void Start()
    {
        if (!PlayerPrefs.HasKey("curVolume"))
        {
            PlayerPrefs.SetFloat("curVolume", audioSlider.value);
        }

        else
        {
            audioSlider.value = PlayerPrefs.GetFloat("curVolume");
        }
    }


    // Using the audioMixer, set the game volume relative to the value in the slider.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("curVolume", volume);
    }


    // When the return button is pressed, close this menu.
    public void ReturnPressed()
    {
        Close();
    }
}