using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLanguage : MonoBehaviour
{
    [SerializeField] private string[] _texts;
    private Text _text;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        GlobalSettings.onLanguageChanged += ChangeLanguage;
        ChangeLanguage();
    }

    private void OnDisable()
    {
        GlobalSettings.onLanguageChanged -= ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        _text.text = _texts[(int)GlobalSettings.language];
    }
}
