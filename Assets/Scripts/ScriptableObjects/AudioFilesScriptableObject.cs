using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Audio Files", menuName = "ScriptableObjects/Audio Files")]
public class AudioFilesScriptableObject : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips;
    public AudioClip[] AudioClips { get => _audioClips; }

    /// <summary>
    /// Returns a random clip from this object's array
    /// </summary>
    public AudioClip GetRandomClip()
    {
        // Don't return anything if there aren't any clips to look through
        if (AudioClips.Length == 0)
            return null;

        int index = Random.Range(0, AudioClips.Length);
        return AudioClips[index];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AudioFilesScriptableObject))]
class AudioFilesScriptableObjectEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        PursueTargetBehaviour script = target as PursueTargetBehaviour;

        // Declare help text
        string infoText = "This object serves as a container for audio files";

        // Display help text
        _showText = EditorGUILayout.BeginFoldoutHeaderGroup(_showText, "Info");
        if (_showText)
        {
            EditorGUILayout.HelpBox(infoText, MessageType.Info);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Display base inspector gui
        base.OnInspectorGUI();
    }
}
#endif