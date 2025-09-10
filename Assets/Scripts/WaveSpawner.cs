using UnityEngine;

using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name = "Wave";
        public int count = 15;             // hoeveel enemies in deze wave
        public float spawnRate = 1.0f;    // per seconde
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public Enemy enemyPrefab;

    public float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;
    private bool spawning = false;

    private void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Geen spawnPoints ingesteld!");
        }
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        // wacht even voor de eerste wave
        yield return new WaitForSeconds(2f);

        // in RunWaves():
        while (currentWaveIndex < waves.Length)
        {
            EnemyCounter.ResetCount();
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            // wacht tot alle enemies dood zijn
            while (EnemyCounter.Alive > 0)
                yield return null;

            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
                yield return new WaitForSeconds(timeBetweenWaves);
        }


        Debug.Log("Alle waves voltooid!");
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        spawning = true;
        Debug.Log($"Start {wave.name}");

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f / Mathf.Max(wave.spawnRate, 0.0001f));
        }

        spawning = false;
    }

    // in WaveSpawner.SpawnEnemy():
    private void SpawnEnemy()
    {
        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        print("new spawn");
        //Enemy copyEnemy = Instantiate(enemyPrefab, p.position, p.rotation);
        Enemy copyEnemy = Instantiate(enemyPrefab);
        EnemyCounter.Alive++;
    }

}

