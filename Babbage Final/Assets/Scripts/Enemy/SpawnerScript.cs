using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public List<WaveEnemy> enemies; // List of enemy types in this wave
}

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int count;
    public float delayBetweenSpawns;
}

public class SpawnerScript : MonoBehaviour
{
    public List<Wave> waves;
    public Transform[] spawnPoints; // Predefined spawn spots
    public float minTimeBetweenWaves = 5f;
    public float maxTimeBetweenWaves = 10f;
    public AudioClip waveIncomingSFX;

    private bool isSpawningWave = false;

    void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    IEnumerator WaveSpawner()
    {
        while (true)
        {
            if (!isSpawningWave)
            {
                isSpawningWave = true;

                float waitTime = Random.Range(minTimeBetweenWaves, maxTimeBetweenWaves);
                yield return new WaitForSeconds(waitTime);

                int randomWaveIndex = Random.Range(0, waves.Count);
                Wave waveToSpawn = waves[randomWaveIndex];

                // Play wave incoming SFX
                if (waveIncomingSFX)
                    AudioSource.PlayClipAtPoint(waveIncomingSFX, transform.position);

                yield return new WaitForSeconds(1f); // Optional pause after SFX

                yield return StartCoroutine(SpawnWave(waveToSpawn));
                isSpawningWave = false;
            }

            yield return null;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.waveName);

        foreach (WaveEnemy waveEnemy in wave.enemies)
        {
            for (int i = 0; i < waveEnemy.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(waveEnemy.enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(waveEnemy.delayBetweenSpawns);
            }
        }
    }
}
