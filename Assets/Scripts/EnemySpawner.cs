using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    WaveConfigSO currentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        foreach (WaveConfigSO waveConfig in waveConfigs)
        {

            currentWave = waveConfig;
            for (int i = 0; i < waveConfig.GetEnemyCount(); i++)
            {
                Instantiate(waveConfig.GetEnemyPrefab(i),
                    waveConfig.GetStartingPoint().position,
                    Quaternion.identity, this.transform);
                yield return new WaitForSeconds(waveConfig.GetRandomSpawnTime());
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
