using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeUIBehaviour : MonoBehaviour
{
    private Image _image;
    private Animator _animator;

    private void Start()
    {
        // Get references to child image's components. Pass true to get them while inactive
        _image = GetComponentInChildren<Image>(true);
        _animator = GetComponentInChildren<Animator>(true);
        
        // Turn on the image
        _image.gameObject.SetActive(true);

        // Disable image raycast target to prevent it from blocking other UI elements
        _image.raycastTarget = false;
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}
