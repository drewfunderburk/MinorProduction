using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectionScreenBehaviour : MonoBehaviour
{
    public bool easyPlanetSelected = false;

    [SerializeField] private bool _showDebugGizmos = true;
    [SerializeField] private RandomizePlanetBehaviour _easyPlanet;
    [SerializeField] private RandomizePlanetBehaviour _hardPlanet;
    [SerializeField] private RandomizePlanetBehaviour _movingPlanet = null;
    [SerializeField] private PlayerMovementBehaviour _player;
    [SerializeField] private float _selectionRadius;
    [SerializeField] private float _selectionDuration;

    private RandomizePlanetBehaviour _selectedPlanet;
    private float _selectionTimer = 0;

    public RandomizePlanetBehaviour SelectedPlanet { get => _selectedPlanet; private set => _selectedPlanet = value; }

    private void Update()
    {
        if(SelectedPlanet == _hardPlanet)
        {
            easyPlanetSelected = false;
        }
        else { easyPlanetSelected = true; }

        // Don't bother with anything if we aren't in planet select
        if (GameManagerBehaviour.Instance.GameState != GameManagerBehaviour.GameStates.PLANET_SELECT)
            return;

        // Check if we're close enough to a planet to select it
        FindSelectedPlanet();

        // If a planet has been selected, start the timer
        if (SelectedPlanet)
            _selectionTimer += Time.deltaTime;
        // Otherwise, reset it
        else
            _selectionTimer = 0;

        // If a planet is selected and the timer is complete, go to that planet
        if (SelectedPlanet && _selectionTimer > _selectionDuration)
        {
            // Increase level based on which planet was selected
            if (SelectedPlanet == _easyPlanet)
            {
                GameManagerBehaviour.Instance.IncreaseLevelEasy();
                // Set health to max
                _player.GetComponent<PlayerHealthBehaviour>().RegenHealth(99999);
            }
            else if (SelectedPlanet == _hardPlanet)
                GameManagerBehaviour.Instance.IncreaseLevelHard();

            // Change game state
            GameManagerBehaviour.Instance.GameState = GameManagerBehaviour.GameStates.WARP;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!_showDebugGizmos)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_easyPlanet.transform.position, _selectionRadius);
        Gizmos.DrawWireSphere(_hardPlanet.transform.position, _selectionRadius);
    }

    /// <summary>
    /// Finds which planet is within selectionRadius and selects it
    /// </summary>
    private void FindSelectedPlanet()
    {
        // Store planets position as if on the same y as player
        Vector3 easyPlanet = _easyPlanet.transform.position;
        easyPlanet.y = _player.transform.position.y;
        Vector3 hardPlanet = _hardPlanet.transform.position;
        hardPlanet.y = _player.transform.position.y;

        // Get distances to planets
        float distanceToEasyPlanet = Vector3.Distance(_player.transform.position, easyPlanet);
        float distanceToHardPlanet = Vector3.Distance(_player.transform.position, hardPlanet);

        // Find which planet should be selected, if any
        if (distanceToEasyPlanet < _selectionRadius)
        {
            SelectedPlanet = _easyPlanet;
            _easyPlanet.Selected = true;
            _hardPlanet.Selected = false;
        }
        else if (distanceToHardPlanet < _selectionRadius)
        {
            SelectedPlanet = _hardPlanet;
            _hardPlanet.Selected = true;
            _easyPlanet.Selected = false;
        }
        else
        {
            SelectedPlanet = null;
            _hardPlanet.Selected = false;
            _easyPlanet.Selected = false;
        }
    }

    public void UpdateMovingPlanetToSelected()
    {
        if (_movingPlanet)
        {
            _movingPlanet.GeneratedColor = _selectedPlanet.GeneratedColor;
            _movingPlanet.MatchedColor = _selectedPlanet.MatchedColor;
            _movingPlanet.AtmosphereColor = _selectedPlanet.AtmosphereColor;
        }
    }
}
