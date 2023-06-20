using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] private bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        { 
           yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i <= waveConfigs.Count - 1; i++)
        {
            var currentWave = waveConfigs[i];
            yield return (SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i <= waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
                              waveConfig.GetEnemyPrefab(),
                                    waveConfig.GetwayPoints()[0].transform.position,
                                    Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
    
}
