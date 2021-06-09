using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectionScreenBehaviour : MonoBehaviour
{
    [SerializeField] private RandomizePlanetBehaviour _easyPlanet;
    [SerializeField] private RandomizePlanetBehaviour _hardPlanet;
    [SerializeField] private PlayerMovementBehaviour _player;
    [SerializeField] private float _selectionRadius;
    [SerializeField] private float _selectionDuration;

    private RandomizePlanetBehaviour _selectedPlanet;
    private float _selectionTimer = 0;

    private void Update()
    {
        // Check if we're close enough to a planet to select it
        FindSelectedPlanet();

        // If a planet has been selected, start the timer
        if (_selectedPlanet)
            _selectionTimer += Time.deltaTime;
        // Otherwise, reset it
        else
            _selectionTimer = 0;

        // If a planet is selected and the timer is complete, go to that planet
        if (_selectedPlanet && _selectionTimer > _selectionDuration)
        {
            // Increase level based on which planet was selected
            if (_selectedPlanet == _easyPlanet)
                GameManagerBehaviour.Instance.IncreaseLevelEasy();
            else if (_selectedPlanet == _hardPlanet)
                GameManagerBehaviour.Instance.IncreaseLevelHard();

            // Change game state
            GameManagerBehaviour.Instance.GameState = GameManagerBehaviour.GameStates.WARP;
        }
    }

    /// <summary>
    /// Finds which planet is within selectionRadius and selects it
    /// </summary>
    private void FindSelectedPlanet()
    {
        // Get distances to planets
        float distanceToEasyPlanet = Vector3.Distance(_player.transform.position, _easyPlanet.transform.position);
        float distanceToHardPlanet = Vector3.Distance(_player.transform.position, _hardPlanet.transform.position);

        // Find which planet should be selected, if any
        if (distanceToEasyPlanet < _selectionRadius)
        {
            _selectedPlanet = _easyPlanet;
            _easyPlanet.Selected = true;
            _hardPlanet.Selected = false;
        }
        else if (distanceToHardPlanet < _selectionRadius)
        {
            _selectedPlanet = _hardPlanet;
            _hardPlanet.Selected = true;
            _easyPlanet.Selected = false;
        }
        else
            _selectedPlanet = null;
    }
}
