using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private AudioClip _roarSound;
    [SerializeField] private AudioClip _attackSound;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector3 _movementDirection;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>(); 
        _startPosition = _rigidbody.transform.position;
        StartCoroutine(AudioTools.PlayLoopSound(_audioSource, _roarSound, 5f, 10f));
    }

    private void Update()
    {
        if (GetDistanceFromPlayer() > 1.2f)
            _animator.SetTrigger("idle");
        else if (_playerDetector.IsPlayerInArea && GetDistanceFromPlayer() <= 1.2f)
            _animator.SetTrigger("attack");
        Move();
    } 
    private void Move()
    {
        SetDirection();
        SetOrientation();
        transform.parent.position += _movementDirection;
    }
    private void SetDirection()
    {
        if (_playerDetector.IsPlayerInArea && GetDistanceFromPlayer() > 1f)
        {
            _movementDirection = (Player.Instance.GetComponent<Rigidbody2D>().transform.position - _rigidbody.transform.position).normalized * Time.deltaTime * _movementSpeed;
        }
        else if (!_playerDetector.IsPlayerInArea && (_rigidbody.transform.position - _startPosition).magnitude > 0.2f)
            _movementDirection = (_startPosition - _rigidbody.transform.position).normalized * Time.deltaTime * _movementSpeed;
        else
            _movementDirection = Vector3.zero;
    }

    private void SetOrientation()
    {     
        if (Vector2.Angle(_movementDirection, new Vector2(1f, 0f)) < 90f)
        { 
            _spriteRenderer.flipX = false;
        }   
        else
        {
            _spriteRenderer.flipX = true;
        }

        if (_playerDetector.IsPlayerInArea)
        {
            if (Player.Instance != null)
            {
                if (_rigidbody.transform.position.x < Player.Instance.GetComponent<Rigidbody2D>().transform.position.x)
                {
                    _spriteRenderer.flipX = false;
                }
                else
                {
                    _spriteRenderer.flipX = true;
                }
            }  
        }  
    }

    private void Attack(string str)
    {
        if (Player.Instance != null)
        {
            AudioTools.PlaySound(_audioSource, _attackSound);
            if ((Player.Instance.GetComponent<Rigidbody2D>().transform.position - _rigidbody.transform.position).magnitude < 1.5f)
                Player.Instance.ApplyDamage(_damage);
        }
    }

    private float GetDistanceFromPlayer()
    {
        if (Player.Instance != null)
            return (Player.Instance.GetComponent<Rigidbody2D>().transform.position - _rigidbody.transform.position).magnitude;
        else
            return Mathf.Infinity;
    }
}
