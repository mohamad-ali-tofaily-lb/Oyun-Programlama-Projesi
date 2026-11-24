using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform player;
    public float timeBetweenSpawns = 10f;
    public float startDelay = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, timeBetweenSpawns);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, chosenPoint.position, chosenPoint.rotation);

        // Assign the player to the new AI Brain and Controller
        EnemyAI aiScript = newEnemy.GetComponent<EnemyAI>();
        if (aiScript != null) aiScript.player = player;

        EnemyController controllerScript = newEnemy.GetComponent<EnemyController>();
        if (controllerScript != null) controllerScript.player = player;
    }
}