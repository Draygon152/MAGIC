// Written by Kevin Chao

public class SingleplayerLobbyMenu : LobbyMenu<SingleplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 1;

        base.Start();
    }

    public override void SetUpPlayerManager()
    {
        base.SetUpPlayerManager();

        //with the player manager set up, lobby menu is no longer needed
        Menu<LobbyMenu<SingleplayerLobbyMenu>>.Close();
    } 
}