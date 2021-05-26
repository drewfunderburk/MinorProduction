using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class MenuBehaviour : MonoBehaviour
{
    public Canvas Menu { get; protected set; }

    protected virtual void Awake()
    {
        Menu = GetComponent<Canvas>();
    }

    /// <summary>
    /// Restart's the scene with a delay
    /// </summary>
    public virtual void RestartScene(float delay = 0)
    {
        StartCoroutine(RestartSceneCoroutine(delay));
    }

    /// <summary>
    /// Coroutine for scene restarting
    /// </summary>
    protected virtual IEnumerator RestartSceneCoroutine(float delay)
    {
        // Wait however long is specified
        yield return new WaitForSeconds(delay);

        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit application or exit play mode
    /// </summary>
    public virtual void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
