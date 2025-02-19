using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _minXPosition;
    [SerializeField] private float _maxXPosition;
    private Rigidbody2D _rigidbody;
    private Vector3 _direction;
    private bool _isPlayerStanding;
    private AudioSource _audioSource;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _direction = new Vector3(1f, 0f, 0f);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transform.position.x <= _minXPosition)
            _direction = new Vector3(1f, 0f, 0f);
        else if (transform.position.x >= _maxXPosition)
            _direction = new Vector3(-1f, 0f, 0f);
        
        transform.position += _direction * Time.deltaTime * _movementSpeed;
        if (_isPlayerStanding)
            Player.Instance.gameObject.GetComponent<Rigidbody2D>().transform.position += _direction * Time.deltaTime * _movementSpeed;
        AudioTools.PlaySound(_audioSource);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isPlayerStanding = true;
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isPlayerStanding = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(_minXPosition, transform.position.y), new Vector2(_maxXPosition, transform.position.y));
    }
}
