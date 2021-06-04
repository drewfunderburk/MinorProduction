using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _scoreText.text = GameManagerBehaviour.Instance.Score.ToString();
    }
}
