// Written by Marc Hagoriles
// Modified by Kevin Chao

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;

    [SerializeField] private AudioClip inGameMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioClip defeatMusic;

    // Make SoundManager a Singleton.
    public static SoundManager Instance
    {
        get;
        private set;
    }



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void OnDestroy()
    {
        Instance = null; 
    }


    private void Start()
    {
        EventManager.Instance.Subscribe(EventTypes.Events.GameStart, PlayInGameMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.Victory, PlayVictoryMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.Defeat, PlayDefeatMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, ResetMusic);
        
        if (PlayerPrefs.HasKey("curVolume"))
        {
            audioPlayer.outputAudioMixerGroup.audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("curVolume")) * 20);
        }

        PlayMusicWithLoop(menuMusic);
    }


    private void PlayMusic(AudioClip audio)
    {
        if (audioPlayer.isPlaying)
        {
            audioPlayer.Stop();
        }

        audioPlayer.clip = audio;
        audioPlayer.loop = false;
        audioPlayer.Play();
    }


    private void PlayMusicWithLoop(AudioClip audio)
    {
        PlayMusic(audio);
        audioPlayer.loop = true;
    }


    // Play the in-game music when the GameStart event is triggered.
    private void PlayInGameMusic()
    {
        PlayMusicWithLoop(inGameMusic);
    }


    // Play the victory music when the Victory event is triggered.
    private void PlayVictoryMusic()
    {
        PlayMusic(victoryMusic);
    }


    // Play the defeat music when the Defeat event is triggered.
    private void PlayDefeatMusic()
    {
        PlayMusic(defeatMusic);
    }


    // When the ResetGame event is triggered, play the menu music again.
    private void ResetMusic()
    {
        PlayMusicWithLoop(menuMusic);
    }
}