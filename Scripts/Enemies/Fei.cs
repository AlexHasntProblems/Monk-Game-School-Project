using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fei : MonoBehaviour, IDamagable
{
    [SerializeField] private float _healthPoint;
    [SerializeField] private GameObject _spirit;
    [SerializeField] private PlayerDetector _playerDetector;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (_playerDetector.IsPlayerInArea)
            _animator.SetTrigger("invoking");
        else
            _animator.SetTrigger("idle");
        SetOrientation();
    }

    private void InvokeSpirit()
    {
        float height = 5f;
        float _spiritPositionX = (Player.Instance.transform.position.y + height) / _spirit.GetComponent<Spirit>().Speed * Player.Instance.GetComponent<Rigidbody2D>().velocity.x + Player.Instance.transform.position.x;
        Instantiate(_spirit, new Vector3(_spiritPositionX, Player.Instance.transform.position.y + height, 0f), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(_spirit, new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + height, 0f), Quaternion.Euler(0f, 0f, 0f));
    }

    public void ApplyDamage(float damage)
    {
        _healthPoint -= damage;
        if (_healthPoint <= 0f)
            Destroy(gameObject);
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
}
