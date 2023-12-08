using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField]  private List<EnemyWaveInfo> _enemyWaveInfo;
  [SerializeField] private float delaySpawnTime;
  [SerializeField] private List<Transform> spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWithDelay(0));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnEnemyWithDelay(int index)
    {
        
        int i = index;
        var random = Random.Range(0, spawnPoints.Count);
        if (i >= _enemyWaveInfo.Count)
            yield break; 
        SpawnEnemy(_enemyWaveInfo[i].GetRandomEnemy(),spawnPoints[random]);
       yield return new WaitForSeconds(_enemyWaveInfo[i].RemainWaveTime);
       i++;
       StartCoroutine(SpawnEnemyWithDelay(i));
    }

    private void SpawnEnemy(GameObject enemy, Transform spawnPos)
    {
        Instantiate(enemy, spawnPos.position, Quaternion.identity);
    }
  
    
}
[Serializable]
public class EnemyWaveInfo
{
    public int WaveIndex;
    public List<EnemySpawn> EnemySpawns;
    public float RemainWaveTime;

    public GameObject GetRandomEnemy()
    {
        var random = Random.Range(0, EnemySpawns.Count);
        return EnemySpawns[random].Enemy;
    }
}

[Serializable]
public class EnemySpawn
{
    public GameObject Enemy;
}
