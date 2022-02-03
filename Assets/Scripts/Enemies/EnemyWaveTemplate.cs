#undef DEBUG

using System.Collections.Generic;
using UnityEngine;

//Written by Lawson McCoy
[CreateAssetMenu(fileName = "Wave", menuName = "Enemy_Wave")]
public class EnemyWaveTemplate : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;    // A list of all the enemy prefabs to be spawned
    [SerializeField] private List<Transform> enemySpawnPoints; // A list of all the spawn location for the enemies (parallel to enmeyPrefab)



    // Validates the contents of enemyPrefabs and enemySpawnPoints, quits game
    // if lists are not of equal length.
    private void ValidateInput()
    {
        // Make sure the number of enemyPrefabs matches the number of enemySpawns
        if (enemyPrefabs.Count != enemySpawnPoints.Count)
        {
            Debug.LogError("The number of enemies and spawn points do not match for " + name, this);
#if (DEBUG)
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }


    // A function to spawn the wave of enemies
    //   Arguments: None
    //   Return value: The total number of enemies that the wave spawned in
    public int SpawnWave(Transform cameraTransform) //unforunately for this play test I need this reference for the health bars
    {
        ValidateInput();

        Debug.Log("Spawning Wave");

        // Spawn all enemies in this wave
        for (int index = 0; index < enemyPrefabs.Count; index++)
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[index], enemySpawnPoints[index].position, enemySpawnPoints[index].rotation);

            //set the health bar to point at the camera
            newEnemy.GetComponentInChildren<EnemyHealthBillboard>().SetCamera(cameraTransform);
        }

        // There was a total of enemyPrefab.Count enemies spawned
        return enemyPrefabs.Count;
    }
}