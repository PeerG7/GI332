using System.Collections;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public Wave[] waves;
    public int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Spawn()
    {
        int Pdx = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[Pdx].position, Quaternion.identity);
        int Idx = Random.Range(0, spawnPoints.Length);
        Instantiate(powerUpPrefab, spawnPoints[Idx].position, Quaternion.identity);

    }
    
    IEnumerator SpawnRoutine()
    {
        
        while (currentWave < waves.Length)
        {
            Debug.Log($"Wave: {currentWave + 1}");
            Wave wave = waves[currentWave];
            yield return new WaitForSeconds(wave.Delaystart);

            for (int i = 0; i < wave.NumberofPowerup; i++)
            {
                Spawn();
            }
            for (int i = 0; i < wave.totalSpawnEnemies; i++)
            {
                int enemyIndex = Random.Range(0, wave.numberOfRandomSpawn);
                Instantiate(enemyPrefab, spawnPoints[enemyIndex].position, Quaternion.identity);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            Debug.Log("Next Wave");
            currentWave++;
        }
        Debug.Log("Finished !!");
    }
}
