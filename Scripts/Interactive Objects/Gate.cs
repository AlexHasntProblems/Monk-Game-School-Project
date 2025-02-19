using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _openingSpeed;
    [SerializeField] private bool _isOpened;
    private float _height = 2f;
    private AudioSource _audioSource;
    public bool IsOpened { private set {} get { return _isOpened; } }
    public bool IsAnimationPlaying { private set; get; }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public IEnumerator Activate()
    {
        AudioTools.PlaySound(_audioSource);
        GetComponent<BoxCollider2D>().enabled = true;
        IsAnimationPlaying = true;
        float direction = 0f;
        float targetYPosition = 0f;

        if (!_isOpened)
        {
            direction = -1f;
            targetYPosition = gameObject.transform.position.y - _height;
        }    
        else
        {
            direction = 1f;
            targetYPosition = gameObject.transform.position.y + _height;
        } 
            
        while ((!_isOpened && gameObject.transform.position.y > targetYPosition) || (_isOpened && gameObject.transform.position.y < targetYPosition))
        {
            gameObject.transform.position += new Vector3(0f, direction, 0f) * Time.deltaTime * _openingSpeed;
            yield return null;
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, targetYPosition, gameObject.transform.position.z);
        _isOpened = !_isOpened;
        if (_isOpened)
            GetComponent<BoxCollider2D>().enabled = false;
        IsAnimationPlaying = false;
        yield return null;     
    }
}
