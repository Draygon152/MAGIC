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

    private int playerCount; // The number of players currently alive in the game
    private int enemyCount;  // The number of enemies currently alive in the game
    private int waveNumber;  // A variable for keeping track of the wave number in the game

    // Make the game manager a singleton
    static public GameManager Instance
    {
        get;
        private set;
    }



    private void Awake()
    {
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
        // spawn in the players
        playerCount = PlayerManager.Instance.SpawnPlayers();

        if (LobbyMenu<SingleplayerLobbyMenu>.Instance != null)
            LobbyMenu<SingleplayerLobbyMenu>.Close();

        else if (LobbyMenu<MultiplayerLobbyMenu>.Instance != null)
            LobbyMenu<MultiplayerLobbyMenu>.Close();

        HUD.Open();

        // Initialize the HUD's player data
        PlayerManager.Instance.InitializeHUD();

        // Set the camera to follow the player
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            CameraSystem.Instance.AddFrameTarget(PlayerManager.Instance.GetPlayerLocation(playerIndex));
        }

        //Set the camera to its starting position.
        CameraSystem.Instance.StartingCamPos();

        // spawn in the first wave
        // Might change later to start a countdown to the first wave
        enemyCount += waves[waveNumber].SpawnWave(CameraSystem.Instance.GetTransform());
        waveNumber++;

        //Set the enemy counter on the HUD
        HUD.Instance.SetEnemyCounter(enemyCount);
    }


    // End the game
    //   bool WinOrLose: A boolean value that is true if the player won the game and false if they lost
    private void EndGame(bool WinOrLose)
    {
        // check if the player won or lost and trigger the corresponding event
        // and update the game state
        if (WinOrLose)
        {
            VictoryGameOver.Open();
        }

        else
        {
            DefeatGameOver.Open();
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
            CameraSystem.Instance.RemoveFrameTarget(PlayerManager.Instance.GetDeadPlayer().transform);
        }
    }


    // The listener for the EnemyDeath event
    public void OnEnemyDeath()
    {
        // decrement enemyCount
        enemyCount--;

        // update enemy counter on HUD
        HUD.Instance.SetEnemyCounter(enemyCount);

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
                enemyCount += waves[waveNumber].SpawnWave(CameraSystem.Instance.GetTransform());
                waveNumber++;
            } //end if (enemyCount <= ENEMIES_REMAINING_BEFORE_NEXT_WAVE)
        }//end else of (if (waveNumber >= waves.Count))

        // update enemy counter on HUD
        HUD.Instance.SetEnemyCounter(enemyCount);
    }


    // Reset the game state when a ResetGame event is notified
    private void OnReset()
    {
        // Reset the game state
        enemyCount = 0;
        playerCount = 0;
        waveNumber = 0;

        // Guarantees gameplay can continue if restarted from pause state
        Time.timeScale = 1;

        // Reset camera frame
        for (int playerIndex = 0; playerIndex < PlayerManager.Instance.GetNumberOfPlayers(); playerIndex++)
        {
            CameraSystem.Instance.RemoveFrameTarget(PlayerManager.Instance.GetPlayerLocation(playerIndex));
        }

        PlayerManager.Instance.ResetPlayers();
    }
}