// Written by Kevin Chao

using UnityEngine.UI;

public class SingleplayerLobbyMenu : LobbyMenu<SingleplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 1;

        base.Start();
    }
}