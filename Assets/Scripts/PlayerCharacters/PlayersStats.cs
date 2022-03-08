using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayersStats : ScriptableObject
{
    public float speed = 10.0f;
    public float turnSpeed = 900.0f;
}
