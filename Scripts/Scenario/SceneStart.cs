using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (UnityEngine.Device.Application.isMobilePlatform)
            GameObject.Find("Canvas").transform.Find("Mobile Controls").gameObject.SetActive(true);
    }
}
