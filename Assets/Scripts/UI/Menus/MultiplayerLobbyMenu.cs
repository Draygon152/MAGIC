// Written by Kevin Chao

public class MultiplayerLobbyMenu : LobbyMenu<MultiplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 2;

        base.Start();
    }

    public override void SetUpPlayerManager()
    {
        base.SetUpPlayerManager();

        //with the player manager set up, lobby menu is no longer needed
        Menu<LobbyMenu<MultiplayerLobbyMenu>>.Close();
    }
}