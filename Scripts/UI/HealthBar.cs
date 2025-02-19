using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Color _maxHealthColor;
    [SerializeField] private Color _minHealthColor;
    private static HealthBar _instance;
    public static HealthBar Instance { private set {} get { return _instance; } } 
    private Image _image;
    private float _maxPlayerHealth;
    private void Start()
    {
        _image = GetComponent<Image>();
        _maxPlayerHealth = Player.Instance.HealthPoint;
        _instance = this;
    }
    public void UpdateHealthBar()
    {
        float percent = Player.Instance.HealthPoint / _maxPlayerHealth;
        if (percent < 0f)
            percent = 0f;

        _image.fillAmount = percent;
        _image.color = Color.Lerp(_minHealthColor, _maxHealthColor, percent);
    }
}
