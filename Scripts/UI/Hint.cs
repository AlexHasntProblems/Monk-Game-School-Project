using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    private static Hint _instance;
    public static Hint Instance
    {
        get { return _instance; }
        private set {}
    }

    public Hint()
    {
        _instance = this;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ChangeText(string text)
    {
        gameObject.GetComponent<Text>().text = text;
    }
    public void ShowHintWithText(string text)
    {
        ChangeText(text);
        Show();
    }
}
