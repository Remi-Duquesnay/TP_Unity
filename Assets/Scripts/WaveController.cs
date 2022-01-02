using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private static WaveController _waveController;

    [SerializeField] List<EnemyWave> waves = new List<EnemyWave>();
    [SerializeField] EnemySpawner spawner;
    private int nextwave;
    private int enemiesRemaining;

    void Awake()
    {
        if (_waveController == null)
        {
            _waveController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        spawner = GetComponent<EnemySpawner>();
        nextwave = 0;
    }

    private void Update()
    {
        if(enemiesRemaining <= 0)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        EnemyWave wave = waves[nextwave];

        GameObject oldMovePattern = GameObject.FindGameObjectWithTag("WavePattern");
        if (oldMovePattern != null)
            DestroyImmediate(oldMovePattern);

        GameObject movePattern = Instantiate(wave.MovePattern);
        movePattern.name = "WavePattern";

        Transform SpawnPoint = GameObject.FindGameObjectWithTag("WaveSpawnPoint").transform;
        enemiesRemaining = wave.EnemyCount;
        StartCoroutine(SpawnEnemy(wave.EnemyCount, SpawnPoint, wave.Enemy, wave.EnemyInterval));
        nextwave++;
        if (nextwave >= waves.Count)
            nextwave = 0;
        
    }

    private IEnumerator SpawnEnemy(int enemyCount, Transform SpawnPoint, GameObject enemy, float interval)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            spawner.Spawn(enemy, SpawnPoint);
            yield return new WaitForSeconds(interval);
        }
    }

    public void removeEnemy()
    {
        enemiesRemaining--;
    }
}
