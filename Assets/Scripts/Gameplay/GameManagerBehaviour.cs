using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerBehaviour : MonoBehaviour
{
    // Singleton instance of this class
    public static GameManagerBehaviour Instance;

    [Tooltip("The game's current level")]
    [SerializeField] private int _level = 0;
    public int Level { get => _level; set => _level = value; }

    // Curve for how quickly enemies spawn per level
    [SerializeField] private AnimationCurve _enemySpawnDelay = null;
    public AnimationCurve EnemySpawnDelay { get => _enemySpawnDelay; }

    // Curve for how many enemies spawn per level
    [SerializeField] private AnimationCurve _enemySpawnCount = null;
    public AnimationCurve EnemySpawnCount { get => _enemySpawnCount; }

    public UnityEvent OnLevelEnd;

    private bool _isGameOver = false;
    public bool IsGameOver { get { return _isGameOver; } }


    private void Awake()
    {
        /* Singleton
         * 
         * If there is no instance, make this the new instance.
         * If there is an instance and it is not this, delete this.
         * 
         * This is to ensure that there is only every one GameManagerBehaviour in a scene,
         *  it is always the oldest one, and that the static variable Instance always refers to it.
         */
        if (!Instance)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
    }
}
