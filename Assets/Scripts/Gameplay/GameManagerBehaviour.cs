using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerBehaviour : MonoBehaviour
{
    // Singleton instance of this class
    public static GameManagerBehaviour Instance;

    [Tooltip("The maximum level allowed")]
    [SerializeField] private int _maxLevel = 20;
    public int MaxLevel { get => _maxLevel; }

    [Tooltip("The game's current level")]
    [SerializeField] private int _level = 0;
    public int Level { get => _level; set => _level = value; }

    // Difficulty curves
    [SerializeField] private DifficultyCurvesScriptableObject _difficultyCurves = null;

    // Curve getters. Returns value at current level
    public float EnemySpawnDelay
    { get { return _difficultyCurves.EnemySpawnDelay.Evaluate((float)_level / _maxLevel); } }

    public int EnemySpawnCount
    { get { return Mathf.RoundToInt(_difficultyCurves.EnemySpawnCount.Evaluate((float)_level / _maxLevel)); } }

    public float DelayBetweenWaveGroups
    { get { return _difficultyCurves.DelayBetweenWaveGroups.Evaluate((float)_level / _maxLevel); } }

    public int WavesInGroup
    { get { return Mathf.RoundToInt(_difficultyCurves.WavesInGroup.Evaluate((float)_level / _maxLevel)); } }

    [SerializeField] private int _score = 0;
    public int Score { get => _score; }

    [Tooltip("Number of times the player has warped to a new planet")]
    [SerializeField] private int _numberOfWarps = 0;
    public int NumberOfWarps { get => _numberOfWarps; set => _numberOfWarps = value; }

    private bool _isGameOver = false;
    public bool IsGameOver { get { return _isGameOver; } }


    private void Awake()
    {
        /* Singleton
         * 
         * If there is no instance, make this the new instance.
         * If there is an instance and it is not this, delete this.
         * 
         * This is to ensure that there is only ever one GameManagerBehaviour in a scene,
         *  it is always the oldest one, and that the static variable Instance always refers to it.
         */
        if (!Instance)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);

        // Clamp level to bounds
        _level = Mathf.Clamp(_level, 0, _maxLevel);
    }

    public void IncreaseScore(int score)
    {
        _score += score;
    }
}
