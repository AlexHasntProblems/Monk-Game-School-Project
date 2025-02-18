using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [SerializeField] private string[] _guideInfoRU;
    [SerializeField] private string[] _guideInfoEN;
    private static Guide _instance;
    private string[][] _guideInfo = new string[2][];

    public static Guide Instance
    {
        get { return _instance; }
        private set {}
    }

    public Guide()
    {
        _instance = this;
    }
    private void Awake()
    {
        _guideInfo[0] = _guideInfoEN;
        _guideInfo[1] = _guideInfoRU;
    }

    private void Start()
    {
        ChangeLanguage();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeLanguage()
    {
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = _guideInfo[(int)GlobalSettings.language][i];
        }
    }
}
