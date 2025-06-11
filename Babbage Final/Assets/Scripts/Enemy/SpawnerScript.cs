using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public List<WaveEnemy> enemies; // List of enemy types in this wave
    public int startingAtWaveCount;
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
    public float minTimeBetweenWaves = 3f;
    public float maxTimeBetweenWaves = 6f;
    public AudioClip waveIncomingSFX;
    public int maxWaveIndex = 0;
    public int waveCount = 0;

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
                AdjustSpawningTime(waveCount);
                float waitTime = Random.Range(minTimeBetweenWaves, maxTimeBetweenWaves);
                yield return new WaitForSeconds(waitTime);

                waveCount++;
                Wave waveToSpawn;
                if (maxWaveIndex < waves.Count - 1 && waveCount >= waves[maxWaveIndex + 1].startingAtWaveCount)
                {
                    maxWaveIndex++;
                    waveToSpawn = waves[maxWaveIndex];
                }
                else
                {
                    waveToSpawn = waves[Random.Range(0, maxWaveIndex)];
                }

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

    private void AdjustSpawningTime(int waveCount)
    {
        switch (waveCount)
        {
            case 25:
                minTimeBetweenWaves = 2.5f;
                break;
            case 30:
                maxTimeBetweenWaves = 5.5f;
                break;
            case 35:
                minTimeBetweenWaves = 2f;
                break;
            case 40:
                maxTimeBetweenWaves = 5f;
                break;
            case 50:
                minTimeBetweenWaves = 1.5f;
                break;
            case 60:
                maxTimeBetweenWaves = 4.5f;
                break;
        }


    }
}
