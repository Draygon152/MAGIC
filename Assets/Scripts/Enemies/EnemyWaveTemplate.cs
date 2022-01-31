using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Wave", menuName = "Enemy_Wave")]
public class EnemyWaveTemplate : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefab;     //A list of all the enemy prefabs to be spawned
    [SerializeField] private List<Transform> enemySpawnPoints; //A list of all the spawn location for the enemies (parallel to enmeyPrefab)

    void Awake()
    {
        //Make sure the number of enemyPrefabs matches the number of enemySpawns
        if (enemyPrefab.Count != enemySpawnPoints.Count)
        {
            throw new Exception("The number of enemies and spawn points do not match for " + this.name);
        }
    }

    /*A function to spawn the wave of enemies

    Arguements - none

    Return value
        The total number of enemies that the wave spawned in
    */
    public int SpawnWave()
    {
        Debug.Log("Spawning Wave");
        //Spawn in all enemies in this wave
        for (int index = 0; index < enemyPrefab.Count; index++)
        {
            Instantiate(enemyPrefab[index], enemySpawnPoints[index].position, enemySpawnPoints[index].rotation);
        }

        //There was a total of enemyPrefab.Count enemies spawned
        return enemyPrefab.Count;
    }


}
