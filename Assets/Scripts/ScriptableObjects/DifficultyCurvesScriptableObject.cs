using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Difficulty Curves", menuName = "ScriptableObjects/Difficulty Curves")]
public class DifficultyCurvesScriptableObject : ScriptableObject
{
    [Tooltip("Delay between individual waves")]
    [SerializeField] private AnimationCurve _enemySpawnDelay = null;
    public AnimationCurve EnemySpawnDelay { get => _enemySpawnDelay; }

    [Tooltip("Number of enemies in a wave")]
    [SerializeField] private AnimationCurve _enemySpawnCount = null;
    public AnimationCurve EnemySpawnCount { get => _enemySpawnCount; }

    [Tooltip("Delay between groups of waves")]
    [SerializeField] private AnimationCurve _delayBetweenWaveGroups = null;
    public AnimationCurve DelayBetweenWaveGroups { get => _delayBetweenWaveGroups; }

    [Tooltip("Number of waves in a group")]
    [SerializeField] private AnimationCurve _wavesInGroup = null;
    public AnimationCurve WavesInGroup { get => _wavesInGroup; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DifficultyCurvesScriptableObject))]
class DifficultyCurvesScriptableObjectEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        PursueTargetBehaviour script = target as PursueTargetBehaviour;

        // Declare help text
        string infoText = "Time = scale between 0 and max level\nValue = desired value at that time";
        string helpText = "Curves should remain within a time of 0 - 1. Exceeding may cause undesired behavior. Set value at points to actual desired value";

        // Display help text
        _showText = EditorGUILayout.BeginFoldoutHeaderGroup(_showText, "Info");
        if (_showText)
        {
            EditorGUILayout.HelpBox(infoText, MessageType.Info);
            EditorGUILayout.HelpBox(helpText, MessageType.Warning);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Display base inspector gui
        base.OnInspectorGUI();
    }
}
#endif