using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private bool _isPlayerInArea;
    public bool IsPlayerInArea { private set {} get { return _isPlayerInArea; } }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            _isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            _isPlayerInArea = false;
    } 
}
