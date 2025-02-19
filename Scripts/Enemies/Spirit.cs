using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    public float Speed { private set {} get { return _speed; } }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(DestroyAfterTimeExpiration());
    }
    private void Start()
    {
        AudioTools.PlaySound(_audioSource);
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.transform.position - Vector3.up * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            Player.Instance.ApplyDamage(_damage);
    }

    private IEnumerator DestroyAfterTimeExpiration()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
