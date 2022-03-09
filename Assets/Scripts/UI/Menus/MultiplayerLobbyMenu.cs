// Written by Kevin Chao

using UnityEngine.UI;

public class MultiplayerLobbyMenu : LobbyMenu<MultiplayerLobbyMenu>
{
    protected override void Start()
    {
        numPlayers = 2;

        base.Start();
    }
}