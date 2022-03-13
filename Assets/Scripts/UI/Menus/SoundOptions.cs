// Written by Marc Hagoriles

using UnityEngine;
using UnityEngine.Audio;


public class SoundOptions : Menu<SoundOptions>
{
    [SerializeField] private AudioMixer audioMixer;



    // Using the audioMixer, set the game volume relative to the value in the slider.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("gameVolume", volume);
    }


    // When the return button is pressed, close this menu.
    public void ReturnPressed()
    {
        Close();
    }
}