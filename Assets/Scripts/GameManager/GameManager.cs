using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private const int NUMBER_OF_WAVES = 1; // The total number of waves the player will play through
    [SerializeField] private const int ENEMIES_REMAINING_BEFORE_NEXT_WAVE = 2; // The number of enemies remaining that will
                                                                               // trigger the next wave, if it is two then
                                                                               // the next wave will spawn when two enemies 
                                                                               // are remaining

    [SerializeField] private GameObject playerPrefab;          // The prefab for the player
    [SerializeField] private Transform playerSpawnPoint;       // The transform for where to spawn the player
    [SerializeField] private GameObject playerCamera;          // The camera that follows the players
    [SerializeField] private GameObject enemyPrefab;           // The prefab for the enemies
    [SerializeField] private List<Transform> enemySpawnPoints; // A list of spawn points for the enemies

    private GameObject players; // A variable to store the player, will likely become an array later
                                // when multiplayer is implemented
    private int playerCount; // The number of players currently alive in the game
    private int enemyCount;  // The number of enemies currently alive in the game
    private int waveNumber;  // A variable for keeping track of the wave number in the game
    
    // Defining an enum for the different game states
    private enum gameState
    {
        start,   // Before the game is running, in Main Menu
        playing, // While playing the game
        pause,   // While playing the game but it is paused
        victory, // When all enemies have been defeated and players have won, in victory menu
        defeat   // When both player have died, in defeat menu
    }
    private gameState state; // A variable for storing the current game state



    // Make the game manager a singleton
    static public GameManager Instance
    {
        get;
        private set;
    }


    void Awake()
    {
        Debug.Log("Game Manager Awake");

        // Set the game state to start when the game begins
        state = gameState.start;

        // Set up Instance
        Instance = this;

        // Initialize the couting variables
        // They can be init to 0, since they will be
        // increment whenever either one is spawned
        playerCount = 0;
        enemyCount = 0;

        // initialize wave number to 0, will be set to one in StartGame
        waveNumber = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Manager Start");

        // Subscribe to events
        // EventManager.Instance;
        // EventManager.Instance.Subscribe(Event.EventTypes.GameStart, StartGame);
        EventManager.Instance.Subscribe(Event.EventTypes.PlayerDeath, OnPlayerDeath);
        EventManager.Instance.Subscribe(Event.EventTypes.EnemyDeath, onEnemyDeath);
    }


    // A function to start the game
    public void StartGame(Element P1Element, Element P2Element)
    {
        Debug.Log("Starting Game");
        
        // spawn in the players
        players = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        playerCount++;

        LobbyMenu.Close();
        HUD.Open();

        // Grab Player HealthManagers
        PlayerHealthManager P1HealthManager = players.GetComponent<PlayerHealthManager>();

        players.GetComponent<MagicCasting>().SetElement(P1Element);

        P1HealthManager.setHealthBarValue = HUD.Instance.SetP1CurHealth;
        P1HealthManager.setHealthBarMax = HUD.Instance.SetP1MaxHealth;

        // Set the camera to follow the player
        playerCamera.GetComponent<CameraSystem>().enabled = true;
        playerCamera.GetComponent<CameraSystem>().player = players.transform;

        // spawn in the first wave
        // Might change later to start a countdown to the first wave
        SpawnWave();

        // set the game state to playing
        state = gameState.playing;
    }


    // End the game
    // bool WinOrLose: A boolean value that is true if the player won the game and false if they lost
    private void EndGame(bool WinOrLose)
    {
        // check if the player won or lost and trigger the corresponding event
        // and update the game state
        if (WinOrLose)
        {
            Debug.Log("Player Victorious");

            state = gameState.victory;
            EventManager.Instance.Notify(Event.EventTypes.PlayerVictory);
        }
        else
        {
            Debug.Log("Player Defeated");

            state = gameState.defeat;
            EventManager.Instance.Notify(Event.EventTypes.PlayerDefeat);
        }
    }


    // The listener for the PlayerDeath event
    public void OnPlayerDeath()
    {
        Debug.Log("Player has died");
        // Some code will be added later for determining which player died
        // for now there is only one player so that player must of died
        // Set the camera to stop following that player
        playerCamera.GetComponent<CameraSystem>().enabled = false;


        // decrement playerCount
        playerCount--;

        // Check if players are still alive
        if (playerCount <= 0)
        {
            // The player lost
            EndGame(false);
        }
    }


    // The listener for the EnemyDeath event
    public void onEnemyDeath()
    {
        Debug.Log("Enemy has died");

        // decrement enemyCount
        enemyCount--;

        // check if final wave
        if (waveNumber >= NUMBER_OF_WAVES)
        {
            Debug.Log("Final wave");
            Debug.Log(enemyCount);

            // On final wave
            // don't spawn more waves
            // Check if player has won
            if (enemyCount <= 0)
            {
                // The player won the game, trigger the event
                EndGame(true);
            }
        }
        else
        {
            Debug.Log("Not final wave");

            // There are more waves to spawn
            // check if they are ready to spawn
            if (enemyCount <= ENEMIES_REMAINING_BEFORE_NEXT_WAVE)
            {
                // ready to spawn next wave
                SpawnWave();
            }
        }
    }


    // Spawn the next wave of enemies
    private void SpawnWave()
    {
        // spawn in the enemies
        Instantiate(enemyPrefab, enemySpawnPoints[0].position, enemySpawnPoints[0].rotation);
        enemyCount++;

        // increment wave number
        waveNumber++;
    }


    void OnDestroy()
    {
        // Marking the GameManager as nonexistent
        Instance = null;
    }
}
