using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersQueue : MonoBehaviour
{
    SpawnerManager spawnerManager;
    [SerializeField] StartWavesQueue _wavesStartTrigger;
    [SerializeField] List<WaveInQueue> waves;
    private void Start()
    {
        StaticWavesValue.EraseData();
        spawnerManager = GetComponent<SpawnerManager>();
        _wavesStartTrigger.playerCollided += StartWaves;
    }
    void StartWaves()
    {
        StaticWavesValue.SetEnemiesLeft(spawnerManager.CountEnemies());
        foreach(WaveInQueue wave in waves)
        {
            StartCoroutine(WaveSpawner(wave.name, wave.timeFromStart));
        }
    }
    IEnumerator WaveSpawner(string wave,int time)
    {
        yield return new WaitForSeconds(time);
        SummonWave(wave);
        yield break;
    }
    void SummonWave(string name)
    {
        spawnerManager.SummonWave(name);
    }

}
[Serializable]
public class WaveInQueue
{
    public string name;
    public int timeFromStart;
}