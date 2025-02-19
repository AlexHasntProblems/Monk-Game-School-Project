using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(GlobalSettings.ChangeSoundVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(GlobalSettings.ChangeSoundVolume);
    }
}