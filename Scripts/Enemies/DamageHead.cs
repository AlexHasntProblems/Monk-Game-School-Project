using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHead : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (_character != null)
                Destroy(_character);
        }
    }
}
