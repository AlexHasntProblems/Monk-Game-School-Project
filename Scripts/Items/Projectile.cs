using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string _targetTag;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;
    private Vector3 _direction;
    private Rigidbody2D _rigidbody;
    private GameObject _origin;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {      
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            _direction = new Vector3(1f, 0f, 0f);
        else
            _direction = new Vector3(-1f, 0f, 0f);

        StartCoroutine(DestroyAfterExpiration());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == _targetTag)
        {
            collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damageable);
                damageable?.ApplyDamage(_damage);
                Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterExpiration()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
