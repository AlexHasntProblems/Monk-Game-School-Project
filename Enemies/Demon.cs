using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour, IDamagable
{
    [SerializeField] private float _healthPoints;
    [SerializeField] private float _meleeDistance;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform[] _swordSpawnOriginsLeft;
    [SerializeField] private Transform[] _swordSpawnOriginsRight;
    [SerializeField] private float _swordSpawnDelay;
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _fireSpawnDelay;
    [SerializeField] private float _minXFireSpawnPosition;
    [SerializeField] private float _maxXFireSpawnPosition;
    [SerializeField] private float _ySpawnFirePosition;
    [SerializeField] private GameObject _scroll;
    private float _currentHeathPoints;
    private static Demon _instance;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    public static Demon Instance { private set {} get { return _instance; } }
    private void Awake()
    {
        _currentHeathPoints = _healthPoints;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _instance = this;
    }
    private void Update()
    {
        SetOrientation();
    }
    private void FixedUpdate()
    {
        if (IsPlayerInMeleeZone())
            _animator.SetTrigger("attack");
        else if (_rigidbody.velocity == Vector2.zero)
            _animator.SetTrigger("idle");     
        else
            _animator.SetTrigger("walk");
    }
    private void ApplyMeleeDamageToPlayer()
    {
        if (IsPlayerInMeleeZone())
            Player.Instance.ApplyDamage(_meleeDamage);
    }
    private void SetOrientation()
    {
        if (Player.Instance != null)
        {
            if (Player.Instance.transform.position.x <= transform.position.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }     
    }

    private bool IsPlayerInMeleeZone()
    {
        return (Player.Instance == null) ? false : ((Player.Instance.GetComponent<Rigidbody2D>().transform.position - _rigidbody.transform.position).magnitude <= _meleeDistance);
    }

    private IEnumerator InvokeSwords()
    {
        while (true)
        {
            Instantiate(_projectile, _swordSpawnOriginsLeft[Random.Range(0, _swordSpawnOriginsLeft.Length)].position, Quaternion.identity).GetComponent<SpriteRenderer>().flipX = false;
            Instantiate(_projectile, _swordSpawnOriginsRight[Random.Range(0, _swordSpawnOriginsRight.Length)].position, Quaternion.identity).GetComponent<SpriteRenderer>().flipX = true;
            yield return new WaitForSeconds(_swordSpawnDelay);
        }    
    }
    public void ApplyDamage(float damage)
    {
        _currentHeathPoints -= damage;
        if (_currentHeathPoints <= 0f)
        {
            DropScroll();
            Destroy(gameObject);
        }  
    }
    private IEnumerator InvokeFire()
    {
        while (true)
        {  
            Instantiate(_fire, new Vector3(Random.Range(_minXFireSpawnPosition, _maxXFireSpawnPosition), _ySpawnFirePosition, 0f), Quaternion.identity);
            yield return new WaitForSeconds(_fireSpawnDelay);
        }
    }

    public void StartBattle()
    {
        StartCoroutine(InvokeFire());
        StartCoroutine(InvokeSwords());
    }

    private void DropScroll()
    {
        _scroll.SetActive(true);
    }
}
