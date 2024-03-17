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
    [SerializeField] private List<EnemyMini> listActiveEnemy;
    [SerializeField] private UnityEvent<EnemyMini> onHaveNewEnemy;
    [SerializeField] private UnityEvent onPauseGame;
    [SerializeField] private UnityEvent onContiueGame;
    [SerializeField] private GamePlayManager gamePlayManager;
    private void Awake()
    {
        listActiveEnemy.Clear();
        Instance = this;
    }
    
    public void InvokeOnPauseGame()
	{
        onPauseGame?.Invoke();
	}
    public void InvokeOnContinueGame()
    {
        onContiueGame?.Invoke();
    }
    // Start is called before the first frame update
    private void Start()
    {
        gamePlayManager = GamePlayManager.Instance;
        StartCoroutine(SpawnEnemyWithDelay(0));
    }
    private IEnumerator SpawnEnemyWithDelay(int index)
    {
        var i = index;
        if (i >= _enemyWaveInfo.Count)
		{
            //gamePlayManager.InvokeOnWinGame();
            yield break;
		}
        for (var j = 0; j < _enemyWaveInfo[i].EnemySpawns.Count; j++)
        {
            var random = Random.Range(0, spawnPoints.Count);
            var delayTime = 0.3f;
            yield return new WaitForSeconds(delayTime);
            var enemyNew = SpawnEnemy(_enemyWaveInfo[i].GetRandomEnemy(), spawnPoints[random]);
            print($"progress bar fill amount {(float)((float)_enemyWaveInfo[i].WaveIndex /(float) _enemyWaveInfo[_enemyWaveInfo.Count - 1].WaveIndex)}");
			gamePlayManager.SetProgressBarFillAmount((float)_enemyWaveInfo[i].WaveIndex / (float)_enemyWaveInfo[_enemyWaveInfo.Count - 1].WaveIndex);
			if (enemyNew == null) yield break;
        }
        yield return new WaitForSeconds(_enemyWaveInfo[i].RemainWaveTime);
        i++;
        StartCoroutine(SpawnEnemyWithDelay(i));
    }
    public void MakeEnemyPause()
	{
        foreach(var enemy in listActiveEnemy)
		{
            if (!enemy) {
                continue; }
            enemy.SetIsPause(true);
		}
	}
    public void MakeEnemyContinue()
    {
        foreach (var enemy in listActiveEnemy)
        {
            if (!enemy)
            {
                continue;
            }
            enemy.SetIsPause(false);
        }
    }
    private GameObject SpawnEnemy(EnemyMini enemy, Transform spawnPos)
    {
        if (enemy == null)
            return null;

        var enemyNew = Instantiate(enemy.gameObject, spawnPos.position, Quaternion.identity);
        listActiveEnemy.Add(enemyNew.GetComponent<EnemyMini>());
        return enemyNew;
    }
	private void OnDisable()
	{
        Instance = null;
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