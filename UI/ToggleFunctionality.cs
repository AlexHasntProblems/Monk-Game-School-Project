using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFunctionality : MonoBehaviour
{
    [SerializeField] private uint _id;
    private Toggle _toggle;
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ChangeValue);
    }
    private void ChangeValue(bool value)
    {
        if (value)
        {
            GlobalSettings.language = (Language)_id;
            GlobalSettings.onLanguageChanged?.Invoke();           
        }      
    }
}
