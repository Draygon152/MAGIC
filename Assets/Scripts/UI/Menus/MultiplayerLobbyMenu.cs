// Written by Kevin Chao

public class MultiplayerLobbyMenu : LobbyMenu<MultiplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 2;

        base.Start();
    }
}