using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will manage the players and their data
public class PlayerManager : MonoBehaviour
{
    private const int NUMBER_OF_PLAYERS = 2; //The total number of players in the game

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

    //enum for refering to player 1 and player 2
    public enum Players
    {
        Player1 = 0,
        Player2 = 1
    }

    private void awake()
    {
        // Set up Instance
        Instance = this;

        //Initialize the player data SO's
        playerData = new PlayerData[NUMBER_OF_PLAYERS];
        playerData[(int)Players.Player1] = ScriptableObject.CreateInstance<PlayerData>(); //create SO for player 1
        playerData[(int)Players.Player2] = ScriptableObject.CreateInstance<PlayerData>(); //create SO for player 2

        //Initialize the player game objects array
        playerGameObject = new Player[NUMBER_OF_PLAYERS];
    }

    //Spawn the players in the game, and return the number of players spawned
    public int SpawnPlayers()
    {
        playerGameObject[(int)Players.Player1] = Instantiate(playerPrefab, playerSpawnPoint.position + spawnOffset, playerSpawnPoint.rotation); //Spawn player 1
        playerGameObject[(int)Players.Player2] = Instantiate(playerPrefab, playerSpawnPoint.position - spawnOffset, playerSpawnPoint.rotation); //Spawn player 2

        // Set player's elemental affinity, assign delegates to player's health bar
        playerGameObject[(int)Players.Player1].SetElement(playerData[(int)Players.Player1].GetElement());
        playerGameObject[(int)Players.Player2].SetElement(playerData[(int)Players.Player2].GetElement());
        playerGameObject[(int)Players.Player1].SetHealthBarDelegates(HUD.Instance.SetP1CurHealth, HUD.Instance.SetP1MaxHealth);
        playerGameObject[(int)Players.Player2].SetHealthBarDelegates(HUD.Instance.SetP2CurHealth, HUD.Instance.SetP2MaxHealth);

        //HUD base spell display
        HUD.Instance.SetP1SpellInfo(playerGameObject[(int)Players.Player1].GetBaseSpell());

        //Set up camera with players
        gameCamera.AddFrameTarget(playerGameObject[(int)Players.Player1].transform);

        return NUMBER_OF_PLAYERS;
    }

    //A getter function for retrieving the player data
    public PlayerData GetPlayerData(Players player)
    {
        return playerData[(int)player];
    }

    //A getter function for retrieving the location of the players
    public Transform GetPlayerLocation(Players player)
    {
        return playerGameObject[(int)player].transform;
    }

    //Reset the players and camera
    public void Reset()
    {
        //reset camera frame
        gameCamera.RemoveFrameTarget(playerGameObject[(int)Players.Player1].transform);
        gameCamera.RemoveFrameTarget(playerGameObject[(int)Players.Player2].transform);

        //destory players
        Destroy(playerGameObject[(int)Players.Player1].gameObject);
        Destroy(playerGameObject[(int)Players.Player2].gameObject);

    }

    private void OnDestroy()
    {
        // Marking the GameManager as nonexistent
        Instance = null;
    }
}
