// Written by Lawson
// Modified by Lizbeth

using System.Collections;
using UnityEngine;

public class NsquelsnaakBehavior : RangeBehavior
{
    [SerializeField] private EnemyBehaviorBase larvaPrefab; // The prefab for spawning the larvae
    [SerializeField] private int spawnTime = 3;             // The time between spawning of larvae
    [SerializeField] private int spawnAmount = 3;           // The amount of larvae that will spawn each time
    [SerializeField] private float spawnRange = 3.0f;       // The maximum range for spawning the larvae

    private bool spawningLarva = false; // A bool to mark if the Nsquelsnaak is currently spawning larvae


    protected override void Awake()
    {
        base.Awake();
    }


    protected override void PerformEnemyBehavior()
    {
        base.PerformEnemyBehavior();

        // Nsquelsnaak action
        // spawn in larva
        if (!spawningLarva)
        {
            StartCoroutine(SpawnLarva());
        }
    } // end PerformBehavior


    private IEnumerator SpawnLarva()
    {
        float randomRotation;  // A variable to store a random spawning direction
        float randomMagnitude; // A variable to store a random spawning distance
        Vector3 spawnPoint;    // A vector to store the larva's spawn point 

        // mark the Nsquelsnaak as spawning larva
        spawningLarva = true;

        // A loop to spawn the correct amount of larva
        for (int index = 0; index < spawnAmount; index++)
        {
            // get random values
            randomRotation = Random.Range(0, 360);
            randomMagnitude = Random.Range(0, spawnRange);

            // Get relative spawn point
            // Starting with the forward vector (1, 0, 0)
            // rotate randomRotation degrees around the up vector (0, 1, 0)
            // then scale by random magnitude
            spawnPoint = (Quaternion.AngleAxis(randomRotation, Vector3.up) * Vector3.forward) * randomMagnitude;

            // Convert to world coordinates of the spawn point
            spawnPoint = this.gameObject.transform.position + spawnPoint;

            // Instantiate the larva
            Instantiate(larvaPrefab, spawnPoint, this.gameObject.transform.rotation);
        }// end for (int index = 0; index < spawnAmount; index++)

        // Wait for cooldown then mark as not spawning
        yield return new WaitForSeconds(spawnTime);
        spawningLarva = false;
    }
}