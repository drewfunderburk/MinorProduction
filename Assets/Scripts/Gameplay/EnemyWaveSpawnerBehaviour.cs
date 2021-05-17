using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawnerBehaviour : MonoBehaviour
{
    [Tooltip("Should enemies be allowed to spawn?")]
    [SerializeField] private bool _allowSpawning = true;
    public bool AllowSpawning { get => _allowSpawning; set => _allowSpawning = value; }

    [Tooltip("Enemies to spawn")]
    [SerializeField] private GameObject[] _enemyPrefabs = null;

    [Tooltip("Target to pass to enemies")]
    [SerializeField] private Transform _target = null;

    [Tooltip("How long to delay between each enemy spawn in a wave")]
    [SerializeField] private float _enemySpawnDelay = 0.2f;

    private List<EnemyBehaviour> _enemies = null;

    // These will need to be hooked into a difficulty curve
    private int _spawnCount = 5;
    private int _spawnDelay = 3;

    private float _spawnTimer = 0;

    /// <summary>
    /// Spawns an enemy from EnemyPrefabs at a given index
    /// </summary>
    private void SpawnEnemyAtIndex(int index)
    {
        GameObject enemy = Instantiate(_enemyPrefabs[index], transform.position, Quaternion.identity);

        EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        if (enemyBehaviour)
            enemyBehaviour.Target = _target;

        _enemies.Add(enemyBehaviour);
    }

    /// <summary>
    /// Spawns a random enemy from EnemyPrefabs
    /// </summary>
    private void SpawnRandomEnemy()
    {
        int index = Random.Range(0, _enemyPrefabs.Length - 1);
        SpawnEnemyAtIndex(index);
    }

    /// <summary>
    /// Spawns a wave of enemies from EnemyPrefabs at a given index
    /// </summary>
    /// <param name="index">Which enemy to spawn</param>
    /// <param name="numEnemies">How many to spawn</param>
    private IEnumerator SpawnIndexWave(int index, int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemyAtIndex(index);
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    /// <summary>
    /// Spawns a wave of random enemies from EnemyPrefabs
    /// </summary>
    /// <param name="numEnemies">How many to spawn</param>
    private IEnumerator SpawnRandomWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    private void Start()
    {
        _spawnTimer = _spawnDelay;
        _enemies = new List<EnemyBehaviour>();
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (AllowSpawning && _spawnTimer > _spawnDelay)
        {
            _spawnTimer = 0;
            StartCoroutine(SpawnRandomWave(_spawnCount));
        }
    }
}
