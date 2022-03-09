// Written by Kevin Chao

public class SingleplayerLobbyMenu : LobbyMenu<SingleplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 1;

        base.Start();
    }
}