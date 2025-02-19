using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundDetector : MonoBehaviour
{
    private bool _isGrounded = true;
    private Surface _surface;
    public bool IsGrounded { private set {} get { return _isGrounded; } }
    public Surface SurfaceType { private set {} get { return _surface; } }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Environment"))
        {
            _isGrounded = true;
            Enum.TryParse(LayerMask.LayerToName(collider.gameObject.layer), out _surface);
        }           
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Environment"))
        {
            _isGrounded  = false;
        }       
    }
}

public enum Surface 
{
    Grass = 0,
    Concrete = 1
}
