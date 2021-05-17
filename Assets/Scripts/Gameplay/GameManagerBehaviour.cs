using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerBehaviour : MonoBehaviour
{
    // Singleton instance of this class
    public static GameManagerBehaviour Instance;
    public UnityEvent OnLevelEnd;

    private bool _isGameOver = false;
    public bool IsGameOver { get { return _isGameOver; } }


    private void Start()
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

    /// <summary>
    /// Invokes the OnLevelEnd event
    /// </summary>
    public void InvokeOnLevelEnd()
    {
        OnLevelEnd.Invoke();
    }

    /// <summary>
    /// Restart's the scene with a delay
    /// </summary>
    public void RestartScene(float delay = 0)
    {
        StartCoroutine(RestartSceneCoroutine(delay));
    }

    /// <summary>
    /// Coroutine for scene restarting
    /// </summary>
    private IEnumerator RestartSceneCoroutine(float delay)
    {
        // Wait however long is specified
        yield return new WaitForSeconds(delay);

        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit application or exit play mode
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
