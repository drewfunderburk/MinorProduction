using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerBehaviour : MonoBehaviour
{
    public enum GameStates
    {
        GAME,
        PLANET_SELECT,
        WARP
    }

    // Singleton instance of this class
    public static GameManagerBehaviour Instance;

    [Tooltip("The game's current state. Changing this invokes events")]
    [SerializeField] private GameStates _gameState = GameStates.GAME;

    [Tooltip("The maximum level before difficulty curves flatten out")]
    [SerializeField] private int _maxLevel = 20;

    [Tooltip("The game's current level")]
    [SerializeField] private int _level = 0;

    // Difficulty curves
    [SerializeField] private DifficultyCurvesScriptableObject _difficultyCurves = null;

    [Space]
    [SerializeField] private int _score = 0;

    [Tooltip("Number of times the player has warped to a new planet")]
    [SerializeField] private int _numberOfWarps = 0;

    [Tooltip("How many levels to increase if easy planet is chosen at Planet Select")]
    [SerializeField] private int _easyPlanetLevelIncrease = 1;

    [Tooltip("How many levels to increase if hard planet is chosen at Planet Select")]
    [SerializeField] private int _hardPlanetLevelIncrease = 3;

    [Space]
    [Tooltip("Level duration in seconds")]
    [SerializeField] private float _levelDuration = 30;


    // Events
    [Space]
    public UnityEvent OnGameEnter;
    public UnityEvent OnGameExit;
    public UnityEvent OnPlanetSelectEnter;
    public UnityEvent OnPlanetSelectExit;
    public UnityEvent OnWarpEnter;
    public UnityEvent OnWarpExit;

    private bool _isGameOver = false;

    public bool IsGameOver { get { return _isGameOver; } }

    public GameStates GameState
    {
        get => _gameState;

        set
        {
            // Call exit events
            switch (_gameState)
            {
                case GameStates.GAME:
                    OnGameExit.Invoke();
                    break;
                case GameStates.PLANET_SELECT:
                    OnPlanetSelectExit.Invoke();
                    break;
                case GameStates.WARP:
                    OnWarpExit.Invoke();
                    break;
            }

            // Set gameState
            _gameState = value;

            // Call enter events
            switch (value)
            {
                case GameStates.GAME:
                    OnGameEnter.Invoke();
                    break;
                case GameStates.PLANET_SELECT:
                    OnPlanetSelectEnter.Invoke();
                    break;
                case GameStates.WARP:
                    OnWarpEnter.Invoke();
                    break;
            }
        }
    }
    public int MaxLevel { get => _maxLevel; }
    public int Level { get => _level; set => _level = value; }

    // Curve getters. Returns value at current level
    public float EnemySpawnDelay
    { get { return _difficultyCurves.EnemySpawnDelay.Evaluate((float)_level / _maxLevel); } }

    public int EnemySpawnCount
    { get { return Mathf.RoundToInt(_difficultyCurves.EnemySpawnCount.Evaluate((float)_level / _maxLevel)); } }

    public float DelayBetweenWaveGroups
    { get { return _difficultyCurves.DelayBetweenWaveGroups.Evaluate((float)_level / _maxLevel); } }

    public int WavesInGroup
    { get { return Mathf.RoundToInt(_difficultyCurves.WavesInGroup.Evaluate((float)_level / _maxLevel)); } }

    public int Score { get => _score; }
    public int NumberOfWarps { get => _numberOfWarps; set => _numberOfWarps = value; }
    public float LevelDuration { get => _levelDuration; set => _levelDuration = value; }
    public int EasyPlanetLevelIncrease { get => _easyPlanetLevelIncrease; set => _easyPlanetLevelIncrease = value; }
    public int HardPlanetLevelIncrease { get => _hardPlanetLevelIncrease; set => _hardPlanetLevelIncrease = value; }

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

    public void IncreaseLevelEasy()
    {
        _level += _easyPlanetLevelIncrease;
    }

    public void IncreaseLevelHard()
    {
        _level += _hardPlanetLevelIncrease;
    }
}
