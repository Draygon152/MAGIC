// Written by Marc Hagoriles

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioMixerGroup audioMixer;

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
        bgMusic = gameObject.AddComponent<AudioSource>();

        EventManager.Instance.Subscribe(EventTypes.Events.GameStart, PlayInGameMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.Victory, PlayVictoryMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.Defeat, PlayDefeatMusic);
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, ResetMusic);

        bgMusic.outputAudioMixerGroup = audioMixer;

        PlayMusicWithLoop(menuMusic);
    }


    private void PlayMusic(AudioClip audio)
    {
        if (bgMusic.isPlaying)
        {
            bgMusic.Stop();
        }

        bgMusic.clip = audio;
        bgMusic.loop = false;
        bgMusic.Play();
    }


    private void PlayMusicWithLoop(AudioClip audio)
    {
        PlayMusic(audio);
        bgMusic.loop = true;
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