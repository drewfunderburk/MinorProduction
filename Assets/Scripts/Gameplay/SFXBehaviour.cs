using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXBehaviour : MonoBehaviour
{
    [Tooltip("Audio file objects")]
    [SerializeField] private AudioFilesScriptableObject[] _audioFiles;

    [Tooltip("")]
    [SerializeField] private int _selectedAudioFiles = 0;

    [Space]
    [Tooltip("Should the pitch be randomized?")]
    [SerializeField] private bool _randomizePitch = true;
    [SerializeField] private float _minimumPitch = 0.9f;
    [SerializeField] private float _maximumPitch = 1.1f;
    
    [Space]
    [Tooltip("Should the volume be randomized?")]
    [SerializeField] private bool _randomizeVolume = true;
    [SerializeField] private float _minimumVolume = 0.9f;
    [SerializeField] private float _maximumVolume = 1;

    public AudioFilesScriptableObject[] AudioFiles { get => _audioFiles; }
    public bool RandomizePitch { get => _randomizePitch; set => _randomizePitch = value; }
    public float MinimumPitch { get => _minimumPitch; set => _minimumPitch = value; }
    public float MaximumPitch { get => _maximumPitch; set => _maximumPitch = value; }
    public bool RandomizeVolume { get => _randomizeVolume; set => _randomizeVolume = value; }
    public float MinimumVolume { get => _minimumVolume; set => _minimumVolume = value; }
    public float MaximumVolume { get => _maximumVolume; set => _maximumVolume = value; }
    public int SelectedAudioFiles { get => _selectedAudioFiles; set => _selectedAudioFiles = value; }

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play a random file from the AudioFilesScriptableObject at the given index
    /// </summary>
    public void PlayRandomClip(int index)
    {
        // Ensure valid index
        if (index < 0 || index > AudioFiles.Length - 1)
            return;

        // Set the audio source clip
        PlayClip(AudioFiles[index].GetRandomClip());
    }

    /// <summary>
    /// Play a random file from the selected AudioFilesScriptableObject
    /// </summary>
    public void PlayClipFromSelected(int index)
    {
        // Ensure valid index
        if (index < 0 || index > AudioFiles[_selectedAudioFiles].AudioClips.Length - 1)
            return;

        PlayClip(AudioFiles[_selectedAudioFiles].AudioClips[index]);
    }

    /// <summary>
    /// Play a specific audio clip
    /// </summary>
    /// <param name="audioFilesIndex">Which AudioFilesScriptableObject to use</param>
    /// <param name="clipIndex">Which clip in the AudioFilesScriptableObject to play</param>
    public void PlaySpecificClip(int audioFilesIndex, int clipIndex)
    {
        // Ensure valid indices
        if (audioFilesIndex < 0 || audioFilesIndex > AudioFiles.Length - 1)
            return;
        if (clipIndex < 0 || clipIndex > AudioFiles[audioFilesIndex].AudioClips.Length - 1)
            return;

        // Set the audio source clip
        PlayClip(AudioFiles[audioFilesIndex].AudioClips[clipIndex]);
    }

    /// <summary>
    /// Play a clip, randomizing volume and pitch if needed
    /// </summary>
    private void PlayClip(AudioClip clip)
    {
        // Randomize pitch if needed
        if (RandomizePitch)
            _source.pitch = Random.Range(MinimumPitch, MaximumPitch);

        // Randomize volume if needed
        if (RandomizeVolume)
            _source.volume = Random.Range(MinimumVolume, MaximumVolume);

        // Set the audio source clip
        _source.clip = clip;

        // Play the clip
        _source.PlayOneShot(clip);
    }
}
