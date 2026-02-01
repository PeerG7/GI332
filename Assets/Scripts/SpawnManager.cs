using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    /*void Start()
    {
        InvokeRepeating("Spawn", 2 , 1);
    }*/

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Spawn()
    {
        int idx = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[idx].position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        /*Debug.Log("Hello 1" + Time.frameCount);
        yield return null;
        yield return new WaitForSeconds(1);
        Debug.Log("Hello 2" + Time.frameCount);*/

        yield return new WaitForSeconds(2f);
        Spawn();

        int spawncount = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Spawn();
            spawncount++;
            if (spawncount > 5)
            {
                break;
            }

        }

        while (true)
        {
            yield return new WaitForSeconds(5f);
            Spawn();
        }
    }
}
