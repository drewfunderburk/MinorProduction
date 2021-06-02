using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty Curves", menuName = "ScriptableObjects/Difficulty Curves")]
public class DifficultyCurvesScriptableObject : ScriptableObject
{
    // Curve for how quickly enemies spawn per level
    [SerializeField] private AnimationCurve _enemySpawnDelay = null;
    public AnimationCurve EnemySpawnDelay { get => _enemySpawnDelay; }

    // Curve for how many enemies spawn per level
    [SerializeField] private AnimationCurve _enemySpawnCount = null;
    public AnimationCurve EnemySpawnCount { get => _enemySpawnCount; }

    // Curve for how long to wait between groups of waves
    [SerializeField] private AnimationCurve _delayBetweenWaveGroups = null;
    public AnimationCurve DelayBetweenWaveGroups { get => _delayBetweenWaveGroups; }

    // Curves for how many waves are in a group
    [SerializeField] private AnimationCurve _wavesInGroup = null;
    public AnimationCurve WavesInGroup { get => _wavesInGroup; }
}
