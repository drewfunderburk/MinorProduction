using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DButtonBehaviour : MonoBehaviour
{
    private Material _meshMaterial;
    private Button _button;

    private void Start()
    {
        _meshMaterial = GetComponentInChildren<Renderer>().material;
        _button = GetComponent<Button>();
    }
}
