using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreBehaviour : MonoBehaviour
{
    [SerializeField] private float _lerpFactor = 0.1f;
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int score;
        Int32.TryParse(_scoreText.text, out score);

        _scoreText.text = Mathf.RoundToInt(Mathf.Lerp(score, GameManagerBehaviour.Instance.Score, _lerpFactor)).ToString();
    }
}
