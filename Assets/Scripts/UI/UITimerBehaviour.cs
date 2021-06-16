using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimerBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = Mathf.RoundToInt(GameManagerBehaviour.Instance.TimeLeftInLevel).ToString();
    }
}
