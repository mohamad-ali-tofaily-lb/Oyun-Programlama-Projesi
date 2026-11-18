using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject enemyPrefab; // Drag your Sheriff Prefab here
    public Transform[] spawnPoints; // Create empty GameObjects around the map and put them here
    public Transform player; // Drag your Player here
    
    [Header("Timing")]
    public float timeBetweenSpawns = 5f;
    public float startDelay = 2f;

    void Start()
    {
        // Start the spawning loop
        InvokeRepeating("SpawnEnemy", startDelay, timeBetweenSpawns);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        // Pick a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        // Spawn the enemy
        GameObject newEnemy = Instantiate(enemyPrefab, chosenPoint.position, chosenPoint.rotation);

        // Assign the player target to the enemy's AI script
        // (This works for EnemyTest, but your teammate will also need to do this in their AIAgent script)
        EnemyTest enemyScript = newEnemy.GetComponent<EnemyTest>();
        if (enemyScript != null)
        {
            enemyScript.player = player;
        }
        
        // Teammate's AI script will need a similar line:
        // AIAgent aiScript = newEnemy.GetComponent<AIAgent>();
        // aiScript.player = player;
    }
}