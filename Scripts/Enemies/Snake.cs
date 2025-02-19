using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snake : MonoBehaviour, IDamagable
{
    [SerializeField] private float _minXPatrolPosition; 
    [SerializeField] private float _maxXPatrolPosition;
    [SerializeField] private float _maxHealthPoints;
    [SerializeField] private float _damage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _attackRange;
    private bool _isBiting;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _direction;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _currentHealthPoints;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealthPoints = _maxHealthPoints;
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(AudioTools.PlayLoopSound(_audioSource, 7f, 12f));
    }

    private void Start()
    {
        _direction = new Vector3(1f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (IsPlayerInAttackRange())
        {
            Attack();
        }
        else
        {        
            Move();
        } 
    }
    private void Move()
    {
        SetDirectionAndOrientation();
        _rigidbody.transform.position += _direction * Time.deltaTime * _movementSpeed;
        _animator.SetTrigger("crawl");
    }

    private void Attack()
    {
        _animator.SetTrigger("attack");
        if (_isBiting)
        {
            Player.Instance.ApplyDamage(_damage);
            _isBiting = false;
        }          
    }

    public void IsBiting(int isBiting) => _isBiting = Convert.ToBoolean(isBiting);

    private bool IsPlayerInAttackRange()
    {
        foreach(RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(_direction.x, 0f), _attackRange))
        {
            if (hit.collider.tag == "Player")
                return true;
        }
        return false;
    }

    private void SetDirectionAndOrientation()
    {       
        if (gameObject.transform.position.x <= _minXPatrolPosition)
        {
            _direction = new Vector3(1f, 0f, 0f);
            _spriteRenderer.flipX = false;
        }
        else if (gameObject.transform.position.x >= _maxXPatrolPosition)
        {
            _direction = new Vector3(-1f, 0f, 0f);
            _spriteRenderer.flipX = true;
        }
    }

    public void ApplyDamage(float damage)
    {
        _currentHealthPoints -= damage;
        if (_currentHealthPoints <= 0f)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(_minXPatrolPosition, transform.position.y, transform.position.z), new Vector3(_maxXPatrolPosition, transform.position.y, transform.position.z));
    }
}
