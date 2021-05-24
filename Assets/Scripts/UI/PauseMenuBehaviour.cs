using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MenuBehaviour
{
    private PlayerControls _controls;

    /// <summary>
    /// Sets the timescale to 0 and show's the menu
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        Menu.enabled = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Menu.enabled = false;
    }
}
