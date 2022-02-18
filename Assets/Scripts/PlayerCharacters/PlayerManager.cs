//Written by Lawson
//Modification by Liz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will manage the players and their data
public class PlayerManager : MonoBehaviour
{
    private const int NUMBER_OF_PLAYERS = 2; //The total number of players in the game
    public const int PLAYER_1 = 0; //array index of player one
    public const int PLAYER_2 = 1; //array index of player two

    public PlayerData[] playerData; //A simple array of length 2 to store the player data 
    public Player[] playerGameObject; //A simple array of length 2 to store references to the player game objects

    //for spawning the players
    [SerializeField] private Player playerPrefab;  // The prefab for the player
    [SerializeField] private Transform playerSpawnPoint; // The transform for where to spawn the player
    [SerializeField] private CameraSystem gameCamera;
    [SerializeField] private Vector3 spawnOffset = new Vector3(5, 0, 0); //The offset from the spawnPoint the players will spawn
                                                                         //Player one spawns at playerSpawnPoint + spawnOffset
                                                                         //Player two spawns at playerSpawnPoint - spawnOffset

    // Make the player manager a singleton
    static public PlayerManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Debug.Log("Player Manager awake");
        // Set up Instance
        Instance = this;

        //Initialize the player data SO's
        playerData = new PlayerData[NUMBER_OF_PLAYERS];
        playerData[PLAYER_1] = ScriptableObject.CreateInstance<PlayerData>(); //create SO for player 1
        playerData[PLAYER_2] = ScriptableObject.CreateInstance<PlayerData>(); //create SO for player 2

        //Initialize the player game objects array
        playerGameObject = new Player[NUMBER_OF_PLAYERS];
    }

    //Spawn the players in the game, and return the number of players spawned
    public int SpawnPlayers()
    {
        playerGameObject[PLAYER_1] = Instantiate(playerPrefab, playerSpawnPoint.position + spawnOffset, playerSpawnPoint.rotation); //Spawn player 1
        playerGameObject[PLAYER_2] = Instantiate(playerPrefab, playerSpawnPoint.position - spawnOffset, playerSpawnPoint.rotation); //Spawn player 2

        // Set player's elemental affinity, assign delegates to player's health bar
        playerGameObject[PLAYER_1].SetElement(playerData[PLAYER_1].GetElement());
        playerGameObject[PLAYER_2].SetElement(playerData[PLAYER_2].GetElement());

        // Set player's number (Liz's modification)
        playerGameObject[PLAYER_1].PlayerNumber = PLAYER_1;
        playerGameObject[PLAYER_2].PlayerNumber = PLAYER_2;

        return NUMBER_OF_PLAYERS;
    }

    //A function to allow the game to tell the player manager when the HUD is ready to take the player data
    public void InitializePlayerHUD()
    {
        //Set health bars
        playerGameObject[PLAYER_1].SetHealthBarDelegates(HUD.Instance.SetP1CurHealth, HUD.Instance.SetP1MaxHealth);
        playerGameObject[PLAYER_2].SetHealthBarDelegates(HUD.Instance.SetP2CurHealth, HUD.Instance.SetP2MaxHealth);

        //Displays player's base spell
        HUD.Instance.SetP1SpellInfo(playerGameObject[PLAYER_1].GetBaseSpell());
        HUD.Instance.SetP2SpellInfo(playerGameObject[PLAYER_2].GetBaseSpell());
    }

    //A getter function for retrieving the player data
    public PlayerData GetPlayerData(int player)
    {
        return playerData[player];
    }

    //A getter function for retrieving the location of the players
    // To access Player 1, pass 0. To access Player 2, pass 1.
    public Transform GetPlayerLocation(int player)
    {
        return playerGameObject[player].transform;
    }

    //Reset the players and camera
    public void ResetPlayers()
    {
        //reset camera frame
        gameCamera.RemoveFrameTarget(playerGameObject[PLAYER_1].transform);
        gameCamera.RemoveFrameTarget(playerGameObject[PLAYER_2].transform);

        //destory players
        Destroy(playerGameObject[PLAYER_1].gameObject);
        Destroy(playerGameObject[PLAYER_2].gameObject);

    }

    private void OnDestroy()
    {
        // Marking the GameManager as nonexistent
        Instance = null;
    }
}
