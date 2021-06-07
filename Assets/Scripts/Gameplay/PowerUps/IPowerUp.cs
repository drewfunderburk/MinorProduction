using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPowerUp
{
    /// <summary>
    /// Apply this PowerUp's functionality
    /// </summary>
    /// <param name="obj">Object to affect</param>
    /// <returns></returns>
    bool ApplyPowerUp(GameObject obj);
}
