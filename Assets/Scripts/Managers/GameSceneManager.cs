using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    //The order the scenes appear in this enum must match the order they appear 
    //in the build settings. If this is the case then the build index of the scene
    //is obtain by casting the enum into an int
    public enum Scenes
    {
        MENU_SCENE,
        LOBBY_SCENE,
        GAME_SCENE
    }

    //This is an enum to state how the scene should be loaded
    //for now there or two modes, loading the scene on the local
    //machine and loading it over a network using photon.
    public enum NetworkSceneMode
    {
        LOCAL,
        NETWORK
    }
    
    //Scene manager is a singleton
    public static GameSceneManager Instance
    {
        get;
        private set;
    }

    //keeps track if the scene that is currently loaded
    public Scenes currentScene
    {
        get;
        private set;
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        //subscribe scene manager events
        SceneManager.sceneLoaded += OnSceneLoaded;

        //subscribe event manager events
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, ReturnToMainMenu); //event might change during photon implementation
    }


    private void OnDestroy()
    {
        // Instance = null;
    }


    public void LoadScene(Scenes scene, NetworkSceneMode loadMode)
    {
        //Currently only local mode supported
        switch (loadMode)
        {
            case NetworkSceneMode.LOCAL:
                currentScene = scene;
                SceneManager.LoadScene((int)scene);
                break;
            case NetworkSceneMode.NETWORK:
                break;
        }
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if game scene then notify the game to be set up
        if (scene.buildIndex == (int)Scenes.GAME_SCENE)
        {
            EventManager.Instance.Notify(EventTypes.Events.GameSetUp);
        }
    }


    public void ReturnToMainMenu()
    {
        LoadScene(Scenes.MENU_SCENE, NetworkSceneMode.LOCAL);
    }
}