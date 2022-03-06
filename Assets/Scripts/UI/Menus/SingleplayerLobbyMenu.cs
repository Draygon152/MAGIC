// Written by Kevin Chao

using UnityEngine.UI;

public class SingleplayerLobbyMenu : LobbyMenu<SingleplayerLobbyMenu>
{
    protected override void Start()
    {
        base.Start();

        numPlayers = 1;
        playerDataList = new PlayerData[numPlayers];
        playerReadyStates = new bool[numPlayers];

        // Primarily for readability, can be extended easily with for loop if the number of players grows
        playerDataList[PlayerManager.PLAYER_1] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
    }
}