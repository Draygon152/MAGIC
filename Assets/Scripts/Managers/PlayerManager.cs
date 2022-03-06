// Written by Lawson
// Modified by Liz and Kevin Chao

using UnityEngine;
using UnityEngine.InputSystem;

//This class will manage the players and their data
public class PlayerManager : MonoBehaviour
{
    public const int PLAYER_1 = 0; // array index of player 1
    public const int PLAYER_2 = 1; // array index of player 2

    private int playerCount;

    private PlayerData[] playerData; // Simple array of length 2 to store the player data 
    private Player[] playerGameObject; // Simple array of length 2 to store references to the player game objects

    //for spawning the players
    [SerializeField] private GameObject playerPrefab;  // The prefab for the player
    [SerializeField] private Transform playerSpawnPoint; // The transform for where to spawn the player
    [SerializeField] private Vector3[] spawnOffset;  // The offset from the spawnPoint the players will spawn

    // Make the player manager a Singleton
    public static PlayerManager Instance
    {
        get;
        private set;
    }

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        playerCount = numberOfPlayers;

        // Initialize the player data SOs
        playerData = new PlayerData[playerCount];
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            playerData[playerIndex] = new PlayerData(); // create SO instance for player 1
        }

        // Initialize the player game objects array
        playerGameObject = new Player[playerCount];
    }


    private void Awake()
    {
        // Set up Instance
        Instance = this;
    }


    private void OnDestroy()
    {
        // Marking the Instance as null
        Instance = null;
    }


    // Spawn the players in the game, return the number of players spawned
    public int SpawnPlayers()
    {
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            // Spawn both players
            playerGameObject[playerIndex] = PlayerInput.Instantiate(playerPrefab, playerIndex: playerIndex, pairWithDevice: playerData[playerIndex].pairedDevice).GetComponent<Player>();
            
            // Move players to their spawn point
            // Note: Because of the use of PlayerInput.Instantiate instead of GameObject.Instantiate (for setting the 
            // player index and pairWithDevice in PlayerInput) I could not pass in the spawn point transform to spawn
            // the players at the right location. As a result they must be moved manually.
            playerGameObject[playerIndex].transform.position = playerSpawnPoint.position + spawnOffset[playerIndex];
            playerGameObject[playerIndex].transform.rotation = playerSpawnPoint.rotation;

            // Set player's elemental affinity, assign delegates to player's health bar
            playerGameObject[playerIndex].SetElement(playerData[playerIndex].ElementalAffinity);

            // Set player identification numbers
            playerGameObject[playerIndex].PlayerNumber = playerIndex;

            //Activate the AI for AI players
            if (playerData[playerIndex].pairedDevice == null)
            {
                playerGameObject[playerIndex].GetComponent<CoopAIBehavior>().enabled = true;

                //disable controls
                playerGameObject[playerIndex].GetComponent<PlayerInput>().enabled = false;
            }
        }

        return playerCount;
    }


    // A function to tell PlayerManager when the HUD is ready to accept player data
    public void InitializeHUD()
    {
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            // Set health bars
            playerGameObject[playerIndex].SetHealthBarDelegates(HUD.Instance.SetPlayerCurHealth, HUD.Instance.SetPlayerMaxHealth);

            // Displays player's base spell
            HUD.Instance.SetPlayerSpellCaster(playerIndex, playerGameObject[playerIndex].GetCaster());
        }
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
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            Destroy(playerGameObject[playerIndex].gameObject);
        }
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

public class PlayerData
{
    public Element ElementalAffinity;
    public InputDevice pairedDevice;
    public bool AI = false;

    public PlayerData()
    {
        ElementalAffinity = null;
        pairedDevice = null;
    }
}