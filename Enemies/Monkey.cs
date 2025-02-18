using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour, IDamagable
{
    [SerializeField] private float _healthPoint;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _angerTime;
    [SerializeField] private float _attackRange;
    private float _attackTimer;
    private float _angerTimer;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(AudioTools.PlayLoopSound(_audioSource, 7f, 10f));
    }

    private void OnDrawGizmos()
    {
        Vector3 direction1 = new Vector3(-1f, 0f, transform.position.z);
        Vector3 direction2 = new Vector3(1f, 0f, transform.position.z);
        Gizmos.DrawRay(transform.position, direction1 * _attackRange);
        Gizmos.DrawRay(transform.position, direction2 * _attackRange);
    }

    private void Update()
    {
        _angerTimer -= Time.deltaTime;
        _attackTimer -= Time.deltaTime;

        if (IsPlayerInAttackRange())
        {
            _angerTimer = _angerTime;
            if (_attackTimer <= 0f)
            {
                Attack();
                _attackTimer = _attackDelay;
            }      
        }
        
        RotateToPlayer();
        
    }
    private void Attack()
    {
        GameObject projectile = Instantiate(_projectile, transform.position, Quaternion.Euler(0f, 0f, 0f));

        if (_spriteRenderer.flipX)
            projectile.GetComponent<SpriteRenderer>().flipX = true;
    }

    private bool IsPlayerInAttackRange()
    {
        Vector2 direction;

        if (_spriteRenderer.flipX)
            direction = new Vector2(-1f, 0f);
        else
            direction = new Vector2(1f, 0f);

        foreach(RaycastHit2D hit in Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), direction, _attackRange))
        {
            if (hit.collider.tag == "Player")
                return true;
        }
        return false;
    }

    private void RotateToPlayer()
    {
        if (Player.Instance != null)
        {
            if (_angerTimer >= 0f)
            {
                if (Player.Instance.transform.position.x <= transform.position.x)
                    _spriteRenderer.flipX = true;
                else
                    _spriteRenderer.flipX = false;
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        _healthPoint -= damage;

        if (_healthPoint <= 0f)
            Destroy(gameObject);    
    }
}
