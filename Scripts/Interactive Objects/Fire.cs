using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;
    [SerializeField] private bool _isHasLifeTime;
    [SerializeField] private float _lifeTime;
    private Animator _animator;
    private Coroutine _applyDamageRoutine;
    private Coroutine _burningRoutine;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _burningRoutine = StartCoroutine(PlayBurningAnimation());
        if (_isHasLifeTime)
            StartCoroutine(DestroyAfterExpiration());
    }

    public IEnumerator GoOutFire()
    {
        StopCoroutine(_burningRoutine);
        yield return null;
        _animator.SetTrigger("going_out");
        yield return null;
        yield return new WaitUntil(() => (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_animator.IsInTransition(0)));
        Destroy(gameObject);
        if (transform.parent != null) 
            Destroy(transform.parent.gameObject);
    }

    private IEnumerator PlayBurningAnimation()
    {
        yield return new WaitUntil(() => (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_animator.IsInTransition(0)));
        _animator.SetTrigger("burning");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _applyDamageRoutine = StartCoroutine(ApplyDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (_applyDamageRoutine != null)
                StopCoroutine(_applyDamageRoutine);
        } 
    }

    private IEnumerator ApplyDamage()
    {
        while (Player.Instance != null && _animator.GetCurrentAnimatorStateInfo(0).IsName("fire_burning"))
        {
            Player.Instance.ApplyDamage(_damage);
            yield return new WaitForSeconds(_delay);
        }
    }
    private IEnumerator DestroyAfterExpiration()
    {
        yield return new WaitForSeconds(_lifeTime);
        StartCoroutine(GoOutFire());
    }
}
