using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private DifficultyCurvesScriptableObject _difficultyCurves = null;

    [Tooltip("PowerUp prefabs")]
    [SerializeField] private GameObject[] _powerUpPrefabs;

    [Tooltip("Width of spawn area")]
    [SerializeField] private float _spawnWidth = 100;

    [SerializeField] private float _minimumSpawnDelay = 10f;
    [SerializeField] private float _maximumSpawnDelay = 20f;

    // List of all spawned powerups
    private List<GameObject> _powerUps = new List<GameObject>();

    private float _spawnDelay = 0;
    private float _spawnTimer = 0;

    private void Start()
    {
        _spawnDelay = Random.Range(_minimumSpawnDelay, _maximumSpawnDelay);
    }

    private void Update()
    {
        // Increment timer
        _spawnTimer += Time.deltaTime;

        // Return if delay not exceeded
        if (_spawnTimer < _spawnDelay)
            return;

        // Reset timer
        _spawnTimer = 0;

        // Spawn a powerUp
        SpawnRandomPowerUp();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw spawn width 
        Gizmos.color = Color.green;
        Vector3 start = new Vector3(-_spawnWidth, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(_spawnWidth, transform.position.y, transform.position.z);
        Gizmos.DrawLine(start, end);
    }

    /// <summary>
    /// Destroy all powerups in scene and clears the list
    /// </summary>
    public void ClearPowerUps()
    {
        foreach (GameObject powerUp in _powerUps)
        {
            // Ensure valid power up
            if (powerUp == null)
                continue;

            Destroy(powerUp);
        }
        _powerUps.Clear();
    }

    /// <summary>
    /// Spawns a random power up at a random position
    /// </summary>
    private void SpawnRandomPowerUp()
    {
        // Select a random xPosition 
        Vector3 position = transform.position;
        position.x = Random.Range(-_spawnWidth, _spawnWidth);

        // Select a random index
        int index = Random.Range(0, _powerUpPrefabs.Length);

        // Spawn a new powerUp
        GameObject powerUp = Instantiate(_powerUpPrefabs[index], position, Quaternion.identity);
        _powerUps.Add(powerUp);
    }
}