// Written by Kevin Chao

public class MultiplayerLobbyMenu : LobbyMenu<MultiplayerLobbyMenu>
{
    protected override void Start()
    {
        base.Start();

        numPlayers = 2;
        playerDataList = new PlayerData[numPlayers];
        playerReadyStates = new bool[numPlayers];

        playerDataList[PlayerManager.PLAYER_1] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
        playerDataList[PlayerManager.PLAYER_2] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_2);
    }
}