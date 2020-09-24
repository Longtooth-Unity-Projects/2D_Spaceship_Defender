using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigurations;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool bIsLooping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (bIsLooping);
    }

    private IEnumerator SpawnAllWaves()
    {
        // using for instead of foreach so we can easily manipulate starting wave from inspector
        for (int waveIndex = startingWave; waveIndex < waveConfigurations.Count; waveIndex++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigurations[waveIndex]));
        }
    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfig)
    {
        List<Transform> waypoints = waveConfig.GetWaypoints();

        for (int index = 0; index < waveConfig.NumOfEnemies; index++)
        {
            GameObject newEnemy = Instantiate(waveConfig.EnemyPrefab, waypoints[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().WaveConfig = waveConfig;
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }
}
