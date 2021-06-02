using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuBehaviour : MenuBehaviour
{
    private bool _isPaused = false;

    /// <summary>
    /// Sets the timescale to 0 and show's the menu
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        Menu.enabled = true;
        _isPaused = true;
    }

    /// <summary>
    /// Sets the timescale to 1 and hide's the menu
    /// </summary>
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Menu.enabled = false;
        _isPaused = false;
    }

    public void TogglePause()
    {
        if (_isPaused)
            UnPauseGame();
        else
            PauseGame();
    }

    protected override void Awake()
    {
        base.Awake();
        Menu.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePause();
    }
}
