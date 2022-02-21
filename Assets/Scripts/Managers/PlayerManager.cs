// Written by Lawson
// Modified by Liz and Kevin Chao

using UnityEngine;
using UnityEngine.InputSystem;

//This class will manage the players and their data
public class PlayerManager : MonoBehaviour
{
    private const int NUMBER_OF_PLAYERS = 2; // Total number of players in the game
    public const int PLAYER_1 = 0; // array index of player 1
    public const int PLAYER_2 = 1; // array index of player 2

    private PlayerData[] playerData; // Simple array of length 2 to store the player data 
    private Player[] playerGameObject; // Simple array of length 2 to store references to the player game objects

    //for spawning the players
    [SerializeField] private GameObject playerPrefab;  // The prefab for the player
    [SerializeField] private Transform playerSpawnPoint; // The transform for where to spawn the player
    [SerializeField] private Vector3 spawnOffset = new Vector3(5, 0, 0); // The offset from the spawnPoint the players will spawn
                                                                         // Player 2 spawns at playerSpawnPoint + spawnOffset
                                                                         // Player 2 spawns at playerSpawnPoint - spawnOffset

    // Make the player manager a Singleton
    static public PlayerManager Instance
    {
        get;
        private set;
    }



    private void Awake()
    {
        Debug.Log("Player Manager Awake");

        // Set up Instance
        Instance = this;

        // Initialize the player data SOs
        playerData = new PlayerData[NUMBER_OF_PLAYERS];
        playerData[PLAYER_1] = ScriptableObject.CreateInstance<PlayerData>(); // create SO instance for player 1
        playerData[PLAYER_2] = ScriptableObject.CreateInstance<PlayerData>(); // create SO instance for player 2

        // Initialize the player game objects array
        playerGameObject = new Player[NUMBER_OF_PLAYERS];
    }


    private void OnDestroy()
    {
        // Marking the Instance as null
        Instance = null;
    }


    // Spawn the players in the game, return the number of players spawned
    public int SpawnPlayers()
    {
        // Spawn both players
        playerGameObject[PLAYER_1] = PlayerInput.Instantiate(playerPrefab, playerIndex: PLAYER_1, pairWithDevice: playerData[PLAYER_1].pairedDevice).GetComponent<Player>();
        playerGameObject[PLAYER_2] = PlayerInput.Instantiate(playerPrefab, playerIndex: PLAYER_2, pairWithDevice: playerData[PLAYER_2].pairedDevice).GetComponent<Player>();
        
        // Move players to their spawn point
        // Note: Because of the use of PlayerInput.Instantiate instead of GameObject.Instantiate (for setting the 
        // player index and pairWithDevice in PlayerInput) I could not pass in the spawn point transform to spawn
        // the players at the right location. As a result they must be moved manually.
        playerGameObject[PLAYER_1].transform.position = playerSpawnPoint.position + spawnOffset;
        playerGameObject[PLAYER_1].transform.rotation = playerSpawnPoint.rotation;
        playerGameObject[PLAYER_2].transform.position = playerSpawnPoint.position - spawnOffset;
        playerGameObject[PLAYER_2].transform.rotation = playerSpawnPoint.rotation;

        // Set player's elemental affinity, assign delegates to player's health bar
        playerGameObject[PLAYER_1].SetElement(playerData[PLAYER_1].GetElement());
        playerGameObject[PLAYER_2].SetElement(playerData[PLAYER_2].GetElement());

        // Set player identification numbers
        playerGameObject[PLAYER_1].PlayerNumber = PLAYER_1;
        playerGameObject[PLAYER_2].PlayerNumber = PLAYER_2;

        return NUMBER_OF_PLAYERS;
    }


    // A function to tell PlayerManager when the HUD is ready to accept player data
    public void InitializeHUD()
    {
        // Set health bars
        playerGameObject[PLAYER_1].SetHealthBarDelegates(HUD.Instance.SetP1CurHealth, HUD.Instance.SetP1MaxHealth);
        playerGameObject[PLAYER_2].SetHealthBarDelegates(HUD.Instance.SetP2CurHealth, HUD.Instance.SetP2MaxHealth);

        // Displays player's base spell
        HUD.Instance.SetP1SpellCaster(playerGameObject[PLAYER_1].GetCaster());
        HUD.Instance.SetP2SpellCaster(playerGameObject[PLAYER_2].GetCaster());
    }


    public Player GetPlayer(int player)
    {
        return playerGameObject[player];
    }


    // A getter function for retrieving the player data
    public PlayerData GetPlayerData(int player)
    {
        return playerData[player];
    }


    // A getter function for retrieving the location of the players
    // To access Player 1, pass 0. To access Player 2, pass 1.
    public Transform GetPlayerLocation(int player)
    {
        return playerGameObject[player].transform;
    }


    // Returns a list of all Players
    public Player[] GetFullPlayerList()
    {
        return playerGameObject;
    }


    // Clear player objects for next game iteration
    public void ResetPlayers()
    {
        // Destroy players
        Destroy(playerGameObject[PLAYER_1].gameObject);
        Destroy(playerGameObject[PLAYER_2].gameObject);
    }


    // This function returns a reference to the dead player if there is one.
    // If there is no dead player it will return null instead. Note that if both
    // players are dead the game will be over, this function is intented to only be called
    // while the game is being played
    public Player GetDeadPlayer()
    {
        // A reference to the dead player
        Player deadPlayer = null; 

        // Check if player 1 is dead
        if (!playerGameObject[PLAYER_1].gameObject.activeSelf)
        {
            deadPlayer = playerGameObject[PLAYER_1];
        }

        // Check if player 2 is dead
        else if (!playerGameObject[PLAYER_2].gameObject.activeSelf)
        {
            deadPlayer = playerGameObject[PLAYER_2];
        }

        return deadPlayer;
    }
}