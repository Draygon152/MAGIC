using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject
{
    public GameObject playerOne;

    public void Initialize(GameObject playerOneInstance)
    {
        playerOne = playerOneInstance;
    }
}
