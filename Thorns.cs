using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;
    private Coroutine _coroutine;
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(ApplyDamage());
        }
    }

    private IEnumerator ApplyDamage()
    {
        while (true)
        {
            if (Player.Instance != null)
                Player.Instance.ApplyDamage(_damage);
            yield return new WaitForSeconds(_delay);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}
