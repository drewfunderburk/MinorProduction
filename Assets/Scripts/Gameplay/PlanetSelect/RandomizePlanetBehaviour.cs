using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePlanetBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _planet;
    [SerializeField] private GameObject _atmosphere;
    [SerializeField] private Color _generatedColor;
    [SerializeField] private Color _matchedColor;
    [SerializeField] private Color _atmosphereColor;
    [SerializeField] private Vector2 _colorClampMinMax;
    [SerializeField] private float _planetSelectModeScale;

    private bool _selected = false;
    public bool Selected { get => _selected; set => _selected = value; }

    private float _initialSize;
    private float _generatedSize;
    private Material _planetMaterial;
    private Material _atmosphereMaterial;

    private void Start()
    {
        _initialSize = transform.localScale.x;
        _atmosphereColor.a = 0.25f;
        _generatedSize = _initialSize;

        // Cache the planet and atmosphere materials
        _planetMaterial = _planet.GetComponent<Renderer>().material;
        _atmosphereMaterial = _atmosphere.GetComponent<Renderer>().material;

        GenerateColorRandomly();
    }

    private void Update()
    {
        // If we are currently in planet select
        if (GameManagerBehaviour.Instance.GameState == GameManagerBehaviour.GameStates.PLANET_SELECT)
        {
            // If this planet is selected, scale it to _planetSelectModeScale
            if (Selected)
                transform.localScale = Vector3.one * _planetSelectModeScale;
            // Otherwise, scale it to _generatedSize
            else
                transform.localScale = Vector3.one * _generatedSize;
        }

        // Set planet and atmosphere materials
        _planetMaterial.SetInt("PlanetSelectMode", Selected ? 1 : 0);
        _planetMaterial.SetInt("isSelected", Selected ? 1 : 0);
        _planetMaterial.SetColor("OceanColor", _generatedColor);
        _planetMaterial.SetColor("LandColor", _matchedColor);
        _planetMaterial.SetColor("AtmoColor", _atmosphereColor);
        _atmosphereMaterial.SetColor("AtmoColor", _atmosphereColor);
    }

    private void SetComplimentaryColor()
    {
        float redValue;
        float greenValue;
        float blueValue;

        redValue = Random.Range(0, 80);
        greenValue = Random.Range(0, 80);
        blueValue = Random.Range(0, 80);

        _generatedColor.r = redValue / 100f;
        _generatedColor.g = greenValue / 100f;
        _generatedColor.b = blueValue / 100f;

        _matchedColor.r = Mathf.Clamp(1 - _generatedColor.r, _colorClampMinMax.x, _colorClampMinMax.y);
        _matchedColor.g = Mathf.Clamp(1 - _generatedColor.g, _colorClampMinMax.x, _colorClampMinMax.y);
        _matchedColor.b = Mathf.Clamp(1 - _generatedColor.b, _colorClampMinMax.x, _colorClampMinMax.y);

        _atmosphereColor.r = Mathf.Clamp(_generatedColor.r + _generatedColor.r / 4f, 0, 1) * 2f;
        _atmosphereColor.g = Mathf.Clamp(_generatedColor.g + _generatedColor.g / 4f, 0, 1) * 2f;
        _atmosphereColor.b = Mathf.Clamp(_generatedColor.b + _generatedColor.b / 4f, 0, 1) * 2f;
    }

    private void SetOffsetColor()
    {

        float redValue;
        float greenValue;
        float blueValue;

        redValue = Random.Range(0, 80);
        greenValue = Random.Range(0, 80);
        blueValue = Random.Range(0, 80);

        _generatedColor.r = redValue / 100f;
        _generatedColor.g = greenValue / 100f;
        _generatedColor.b = blueValue / 100f;

        _matchedColor.r = Mathf.Clamp01(_generatedColor.r + _generatedColor.r / 2f);
        _matchedColor.g = Mathf.Clamp01(_generatedColor.g + _generatedColor.g / 2f);
        _matchedColor.b = Mathf.Clamp01(_generatedColor.b + _generatedColor.b / 2f);

        _atmosphereColor.r = Mathf.Clamp(_generatedColor.r + _generatedColor.r / 4f, 0, 1) * 2f;
        _atmosphereColor.g = Mathf.Clamp(_generatedColor.g + _generatedColor.g / 4f, 0, 1) * 2f;
        _atmosphereColor.b = Mathf.Clamp(_generatedColor.b + _generatedColor.b / 4f, 0, 1) * 2f;
    }
    private void SetAnalogousColor()
    {

        float degree;
        degree = Random.Range(0f, 360f);

        _generatedColor.r = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * (degree / 360) + 0.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);
        _generatedColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * (degree / 360) + 1.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);
        _generatedColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * (degree / 360) + 2.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);

        _matchedColor.r = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 0.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);
        _matchedColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 1.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);
        _matchedColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 2.53f) + 0.5f, _colorClampMinMax.x, _colorClampMinMax.y);

        _atmosphereColor.r = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 0.53f) + 0.5f, 0, 1) * 2f;
        _atmosphereColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 1.53f) + 0.5f, 0, 1) * 2f;
        _atmosphereColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 2.53f) + 0.5f, 0, 1) * 2f;
    }

    public void GenerateColorRandomly()
    {
        int colorSelect = Random.Range(0, 3);

        switch (colorSelect)
        {
            case 0:
                SetComplimentaryColor();
                break;
            case 1:
                SetOffsetColor();
                break;
            case 2:
                SetAnalogousColor();
                break;
        }

        _generatedSize = Random.Range(_initialSize * 0.7f, _initialSize);
    }
}
