// Written by Marc Hagoriles

using UnityEngine;

public class MinimapCameraSystem : MonoBehaviour
{
    [SerializeField] public GameObject Minimap;

    // Float variables for the target X positions of the minimap depending on the game mode. (Singleplayer/Multiplayer)
    private float mpTargetX = -960f;
    private float spTargetX = -120f;

    private RectTransform rt;

    // Make this a singleton.
    public static MinimapCameraSystem Instance
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
        rt = Minimap.GetComponent<RectTransform>();

        // Subscribe function to reset the minimap when the game restarts
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, ResetMinimap);
    }


    // Opens the Singleplayer Render of the minimap.
    public void OpenSinglePlayerMinimap()
    {
        // Debug.Log("Singleplayer button is pressed, minimap should be at top-right");

        Minimap.SetActive(true);
    }
    

    // Opens the Multiplayer Render of the minimap.
    public void OpenMultiplayerMinimap()
    {
        // Debug.Log("Multiplayer button is pressed, minimap should be at top-middle");

        // Set the minimap render's x position to the target position.
        rt.anchoredPosition = new Vector2(mpTargetX, rt.anchoredPosition.y);
        Minimap.SetActive(true);
    }


    // Resets both minimap renders to NOT active, for when the game is over / reset.
    public void ResetMinimap()
    {
        // Debug.Log("Game is reset, minimap should return back to Singleplayer position");

        rt.anchoredPosition = new Vector2(spTargetX, rt.anchoredPosition.y);
        Minimap.SetActive(false);
    }
}