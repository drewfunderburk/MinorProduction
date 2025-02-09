﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerHealthBehaviour _health;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _healthGradient;
    [SerializeField] [Range(0, 1)] private float _lerpFactor = 0.1f;

    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _health.MaxHealth;
        _fill.color = _healthGradient.Evaluate(1f);
    }

    void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, _health.Health, _lerpFactor);
        _fill.color = _healthGradient.Evaluate(_slider.value / _slider.maxValue);
    }
}
