using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab; //THe prefab for the player
    [SerializeField] private Transform playerSpawnPoint; //The tranform for where to spawn the player
    [SerializeField] private GameObject playerCamera; //The camera that follows the players

    private GameObject players; //A variable to store the player, will likely become an array later
                                //when muiltiplayer is implemented
    
    //Defining an enum for the different game states
    private enum gameState
    {
        start, //Before the game is running, in Main Menu
        playing, //While playing the game
        pause, //While playing the game but it is paused
        victory, //When all enemies have been defeated and players have won, in victory menu
        defeat, //When both player have died, in defeat menu
    }
    private gameState state; //A variable for storing the current game state

    private MenuManager menuSystem; //A reference to the menu manager

    //Make the game manager a singleton
    static public GameManager Instance
    {
        get;
        set;
    }

    void Awake()
    {
        //Set the game state to start when the game begins
        state = gameState.start;

        //Set up Instance
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the reference to the menu manager
        menuSystem = MenuManager.Instance;
        Debug.Log(menuSystem == null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //A function to start the game
    public void StartGame()
    {
        Debug.Log("Starting Game");
        //hide the menu manager
        MenuManager.Instance.CloseMenu(MainMenu.Instance);

        //spawn in the players
        players = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as GameObject;

        //Set the camera to follow the player
        playerCamera.GetComponent<CameraSystem>().enabled = true;
        playerCamera.GetComponent<CameraSystem>().player = players.transform;

        //set the game state to playing
        state = gameState.playing;
    }
}
