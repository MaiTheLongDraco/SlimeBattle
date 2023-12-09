using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] private List<EnemyWaveInfo> _enemyWaveInfo;
    [SerializeField] private float delaySpawnTime;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private UnityEvent<EnemyMini> onHaveNewEnemy;
	private void Awake()
	{
        Instance = this;
	}

	// Start is called before the first frame update
	private void Start()
    {
        StartCoroutine(SpawnEnemyWithDelay(0));
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnEnemyWithDelay(int index)
    {
        var i = index;
        var random = Random.Range(0, spawnPoints.Count);
        if (i >= _enemyWaveInfo.Count)
            yield break;
      var enemyNew=  SpawnEnemy(_enemyWaveInfo[i].GetRandomEnemy(), spawnPoints[random]);
        onHaveNewEnemy?.Invoke(enemyNew.GetComponent<EnemyMini>());

        yield return new WaitForSeconds(_enemyWaveInfo[i].RemainWaveTime);
        i++;
        StartCoroutine(SpawnEnemyWithDelay(i));
    }

    public EnemyMini GetRandomEnemy()
    {
        if (_enemyWaveInfo.Count <= 0) return null;
        foreach (var enemyWaveInfo in _enemyWaveInfo)
        {
            if (enemyWaveInfo == null) continue;
            return enemyWaveInfo.GetRandomEnemy();
        }

        return null;
    }

    private GameObject SpawnEnemy(EnemyMini enemy, Transform spawnPos)
    {
        if (enemy == null)
            return null;
        
      var enemyNew=  Instantiate(enemy.gameObject, spawnPos.position, Quaternion.identity);
        return enemyNew;
    }
}

[Serializable]
public class EnemyWaveInfo
{
    public int WaveIndex;
    public List<EnemySpawn> EnemySpawns;
    public float RemainWaveTime;

    public EnemyMini GetRandomEnemy()
    {
        var random = Random.Range(0, EnemySpawns.Count);
        return EnemySpawns[random].GetEnemyMini();
    }
}

[Serializable]
public class EnemySpawn
{
    public GameObject Enemy;

    public EnemyMini GetEnemyMini()
    {
        var enemy = Enemy.GetComponent<EnemyMini>();
        return enemy;
    }
}