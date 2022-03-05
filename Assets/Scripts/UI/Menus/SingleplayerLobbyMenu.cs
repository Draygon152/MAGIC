// Written by Kevin Chao

public class SingleplayerLobbyMenu : LobbyMenu
{
    protected override void Start()
    {
        base.Start();

        numPlayers = 1;
        playerDataList = new PlayerData[numPlayers];
        playerReadyStates = new bool[numPlayers];

        playerDataList[PlayerManager.PLAYER_1] = PlayerManager.Instance.GetPlayerData(PlayerManager.PLAYER_1);
    }
}