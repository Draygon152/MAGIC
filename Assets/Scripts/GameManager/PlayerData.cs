using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Liz
public class PlayerData : ScriptableObject
{
    public GameObject playerOne;

    public void Initialize(GameObject playerOneInstance)
    {
        playerOne = playerOneInstance;
    }
}
