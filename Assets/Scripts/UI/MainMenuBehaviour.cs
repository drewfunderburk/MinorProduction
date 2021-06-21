using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MenuBehaviour
{
    public void GoToNextScene(float delay)
    {
        StartCoroutine(GoToNextSceneCoroutine(delay));
    }

    private IEnumerator GoToNextSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
