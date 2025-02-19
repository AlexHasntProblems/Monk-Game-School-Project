using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour, IInteractive
{
    private static Scroll _instance;
    public static Scroll Instance { get { return _instance; } private set { } }
    private void Awake()
    {
        _instance = this;
    }
    public IEnumerator Interact()
    {
        Destroy(gameObject);
        yield return null;
    }
}
