using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BladeCounter : MonoBehaviour
{
    private static BladeCounter _instance;
    public static BladeCounter Instance { private set {} get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }
    public void UpdateCounter()
    {
        GetComponent<Text>().text = $"{Player.Instance.BladesCount}";
    }
}
