//Written by Lawson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

//This class will manage the players and their data
public class PlayerManager : MonoBehaviour
{
    private const int NUMBER_OF_PLAYERS = 2; //The total number of players in the game
    public const int PLAYER_1 = 0; //array index of player one
    public const int PLAYER_2 = 1; //array index of player two

    private PlayerData[] playerData; //A simple array of length 2 to store the player data 
    private Player[] playerGameObject; //A simple array of length 2 to store references to the player game objects

    //for spawning the players
    [SerializeField] private GameObject playerPrefab;  // The prefab for the player
    [SerializeField] private Transform playerSpawnPoint; // The transform for where to spawn the player
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
        //If you don't have a controller connected the first temporary line will
        //cause an out of bounds error at the start of the game. To play the game
        //either connect a controller or change the first line to also grab the 
        //keyboard. The intent is to have pairedDevice set in Lobby menu, but untill
        //that is the case these temporary lines are needed
        playerData[PLAYER_1].pairedDevice = Gamepad.all[0]; //This line is temporary
        playerData[PLAYER_2].pairedDevice = Keyboard.all[1]; //This line is temporary
        playerGameObject[PLAYER_1] = PlayerInput.Instantiate(playerPrefab, playerIndex: PLAYER_1, pairWithDevice: playerData[PLAYER_1].pairedDevice).GetComponent<Player>(); //Spawn player 1
        playerGameObject[PLAYER_2] = PlayerInput.Instantiate(playerPrefab, playerIndex: PLAYER_2, pairWithDevice: playerData[PLAYER_2].pairedDevice).GetComponent<Player>(); //Spawn player 2
        
        //Move the players to the spawn point
        //Note because of the use of PlayerInput instantiate instead of GameObject instantiate (for setting the 
        //player index and pairWithDevice in PlayerInput) I could not pass in the spawn point transform to spawn
        //the players at the right location. As a result I must move them their now.
        playerGameObject[PLAYER_1].transform.position = playerSpawnPoint.position + spawnOffset;
        playerGameObject[PLAYER_1].transform.rotation = playerSpawnPoint.rotation;
        playerGameObject[PLAYER_2].transform.position = playerSpawnPoint.position - spawnOffset;
        playerGameObject[PLAYER_2].transform.rotation = playerSpawnPoint.rotation;

        // Set player's elemental affinity, assign delegates to player's health bar
        playerGameObject[PLAYER_1].SetElement(playerData[PLAYER_1].GetElement());
        playerGameObject[PLAYER_2].SetElement(playerData[PLAYER_2].GetElement());

        // Pairing devices
        Debug.Log(playerGameObject[PLAYER_1].GetComponent<PlayerInput>().devices.Count);
        // playerData[PLAYER_1].pairedDevice = Gamepad.all[0]; //This line is temporary
        // playerData[PLAYER_2].pairedDevice = Keyboard.all[0]; //This line is temporary
        // playerGameObject[PLAYER_1].GetComponent<PlayerInput>().user.UnpairDevices();
        // playerGameObject[PLAYER_2].GetComponent<PlayerInput>().user.UnpairDevices();
        //         Debug.Log(playerGameObject[PLAYER_1].GetComponent<PlayerInput>().devices.Count);
        // InputUser.PerformPairingWithDevice(playerData[PLAYER_1].pairedDevice, playerGameObject[PLAYER_1].GetComponent<PlayerInput>().user);
        // InputUser.PerformPairingWithDevice(playerData[PLAYER_2].pairedDevice, playerGameObject[PLAYER_2].GetComponent<PlayerInput>().user);
        // // playerGameObject[PLAYER_1].GetComponent<PlayerInput>().user.ActivateControlScheme(null);
        // // playerGameObject[PLAYER_2].GetComponent<PlayerInput>().user.ActivateControlScheme(null);
        Debug.Log(playerGameObject[PLAYER_2].GetComponent<PlayerInput>().devices.Count);


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
    public Transform GetPlayerLocation(int player)
    {
        return playerGameObject[player].transform;
    }

    //Reset the players and camera
    public void ResetPlayers()
    {
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
