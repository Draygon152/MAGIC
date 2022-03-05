// Written by Kevin Chao

using UnityEngine.UI;

public class MultiplayerLobbyMenu : LobbyMenu<MultiplayerLobbyMenu>
{
    protected override void Start()
    {
        base.Start();

        numPlayers = 2;
        playerDataList = new PlayerData[numPlayers];
        playerReadyStates = new bool[numPlayers];

        // Primarily for readability, can be extended easily with for loop if the number of players grows
        playerDataList[PlayerManager.PLAYER_1] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
        playerDataList[PlayerManager.PLAYER_2] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_2);
    }
}