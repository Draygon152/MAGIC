// Written by Marc Hagoriles

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : Menu<SoundOptions>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    private static float curVolume = -1.0f;



    private void Start()
    {
        if (curVolume < 0)
        {
            curVolume = AudioListener.volume;
        }

        audioSlider.value = curVolume;
    }


    // Using the audioMixer, set the game volume relative to the value in the slider.
    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("gameVolume", volume);
        AudioListener.volume = volume;
    }


    // When the return button is pressed, close this menu.
    public void ReturnPressed()
    {
        Close();
    }
}