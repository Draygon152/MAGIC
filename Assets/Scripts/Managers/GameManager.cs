// Written by Lawson
// Modified by Angel, Kevin, Liz, and Marc

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private const int NUMBER_OF_WAVES = 1; // The total number of waves the player will play through
    [SerializeField] private const int ENEMIES_REMAINING_BEFORE_NEXT_WAVE = 1; // The number of enemies remaining that will
                                                                               // trigger the next wave, if it is two then
                                                                               // the next wave will spawn when two enemies 
                                                                               // are remaining

    [SerializeField] private List <EnemyWaveTemplate> waves; // A list of scriptable objects representing the waves that 
                                                             // needs to be spawned into the game

    [SerializeField] private CameraSystem gameCamera; //Decoupling is needed to get rid of this

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


    private void Awake()
    {
        // Set the game state to start when the game begins
        state = gameState.start;

        // Set up Instance
        Instance = this;

        // Initialize the counting variables
        // They can be init to 0, since they will be
        // increment whenever either one is spawned
        playerCount = 0;
        enemyCount = 0;

        // Initialize wave number to 0, will be set to one in StartGame
        waveNumber = 0;
    }


    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to events
        EventManager.Instance.Subscribe(EventTypes.Events.GameStart, StartGame);
        EventManager.Instance.Subscribe(EventTypes.Events.PlayerDeath, OnPlayerDeath);
        EventManager.Instance.Subscribe(EventTypes.Events.EnemyDeath, OnEnemyDeath);
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, OnReset);
    }


    private void OnDestroy()
    {
        // Marking the GameManager as nonexistent
        Instance = null;
    }


    // A function to start the game
    public void StartGame()
    {
        Debug.Log("Starting Game");
        
        // spawn in the players
        playerCount = PlayerManager.Instance.SpawnPlayers();

        LobbyMenu.Close();
        HUD.Open();

        // Initialize the HUD's player data
        PlayerManager.Instance.InitializeHUD();

        // Set the camera to follow the player
        gameCamera.AddFrameTarget(PlayerManager.Instance.GetPlayerLocation(PlayerManager.PLAYER_1));
        gameCamera.AddFrameTarget(PlayerManager.Instance.GetPlayerLocation(PlayerManager.PLAYER_2));

        //Set the camera to its starting position.
        gameCamera.StartingCamPos();

        // spawn in the first wave
        // Might change later to start a countdown to the first wave
        enemyCount += waves[waveNumber].SpawnWave(gameCamera.GetTransform());
        waveNumber++;

        //Set the enemy counter on the HUD
        HUD.Instance.SetEnemyCouter(enemyCount);

        // Update current game state
        state = gameState.playing;
    }


    // End the game
    //   bool WinOrLose: A boolean value that is true if the player won the game and false if they lost
    private void EndGame(bool WinOrLose)
    {
        // check if the player won or lost and trigger the corresponding event
        // and update the game state
        if (WinOrLose)
        {
            Debug.Log("Player Victorious");

            VictoryGameOver.Open(); // Activates the Victory Screen UI.

            state = gameState.victory;
        }

        else
        {
            Debug.Log("Player Defeated");

            DefeatGameOver.Open(); // Activates the Defeat Screen UI.

            state = gameState.defeat;
        }

        EventManager.Instance.Notify(EventTypes.Events.GameOver);
    }


    // The listener for the PlayerDeath event
    public void OnPlayerDeath()
    {
        // decrement playerCount
        playerCount--;


        // Check if players are still alive
        if (playerCount <= 0)
        {
            // The player lost
            EndGame(false);
        }

        else //A player dead but the game isn't over (one player is still alive)
        {
            //remove the dead player from the camera frame
            gameCamera.RemoveFrameTarget(PlayerManager.Instance.GetDeadPlayer().transform);
        }
    }


    // The listener for the EnemyDeath event
    public void OnEnemyDeath()
    {
        // decrement enemyCount
        enemyCount--;

        // update enemy counter on HUD
        HUD.Instance.SetEnemyCouter(enemyCount);

        // check if final wave
        if (waveNumber >= waves.Count)
        {
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
            // There are more waves to spawn
            // check if they are ready to spawn
            if (enemyCount <= ENEMIES_REMAINING_BEFORE_NEXT_WAVE)
            {
                // ready to spawn next wave
                enemyCount += waves[waveNumber].SpawnWave(gameCamera.GetTransform());
                waveNumber++;
            } //end if (enemyCount <= ENEMIES_REMAINING_BEFORE_NEXT_WAVE)
        }//end else of (if (waveNumber >= waves.Count))

        // update enemy counter on HUD
        HUD.Instance.SetEnemyCouter(enemyCount);
    }

    // Reset the game state when a ResetGame event is notified
    private void OnReset()
    {
        // reset the game state
        state = gameState.start;
        enemyCount = 0;
        playerCount = 0;
        waveNumber = 0;

        //reset camera frame
        gameCamera.RemoveFrameTarget(PlayerManager.Instance.GetPlayerLocation(PlayerManager.PLAYER_1));
        gameCamera.RemoveFrameTarget(PlayerManager.Instance.GetPlayerLocation(PlayerManager.PLAYER_2));

        PlayerManager.Instance.ResetPlayers();
    }
}