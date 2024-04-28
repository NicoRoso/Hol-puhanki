using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] List<EnemyWave> _waves = new List<EnemyWave>();
    public Action onEnemiesOver;
    public void SummonWave(int id)
    {
        SpawnWave(_waves[id]);
    }
    public void SummonWave(string name)
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            if (_waves[i].name == name)
            {
                SpawnWave(_waves[i]);
                break;
            }
        }

    }
    public int CountEnemies()
    {
        int result = 0;
        foreach(EnemyWave wave in _waves)
        {
            foreach(EnemyTypeByWave enemyTypeByWave in wave.waveContent)
            {
                result += enemyTypeByWave.amount;
            }
        }
        return result;
    }
    

    void SpawnWave(EnemyWave wave)
    {
        List<GameObject> enemiesInWaveList = new List<GameObject>();
        foreach (EnemyTypeByWave enemyType in wave.waveContent)
        {
            for (int i = 0; i < enemyType.amount; i++)
            {
                enemiesInWaveList.Add(enemyType.enemyType);
            }
        }
        for (int i = 0; i < enemiesInWaveList.Count; i++)
        {
            SendMessageToSpawner(wave._includedSpawners[i % wave._includedSpawners.Count], enemiesInWaveList[i]);
        }

    }


    void SendMessageToSpawner(SpawnerBehaviour spawner,GameObject enemy)
    {
        spawner.SpawnEnemy(enemy);
    }
    private void Start()
    {
        StaticWavesValue.EraseData();
    }
    bool enemiesOver = false;
    private void Update()
    {
        if ((StaticWavesValue.GetLeftEnemies() == 0) && !enemiesOver)
        {
            enemiesOver = true;
            onEnemiesOver?.Invoke();
            Debug.Log("враги кончились");
            Destroy(gameObject);
        }
    }
}
[Serializable]
public class EnemyWave
{
    public string name;
    public List<EnemyTypeByWave> waveContent = new List<EnemyTypeByWave>();
    public List<SpawnerBehaviour> _includedSpawners = new List<SpawnerBehaviour>();
    public bool randomizeOrder = true;
    public float secondsBetweenSpawns;
}


[Serializable]
public class EnemyTypeByWave
{
    public GameObject enemyType;
    public int amount;
}
