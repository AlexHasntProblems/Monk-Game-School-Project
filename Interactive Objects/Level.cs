using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IInteractive
{
    [SerializeField] private Gate[] _gates;
    private Animator _animator;
    public bool IsOn { private set {} get { return _animator.GetBool("isOn"); } }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public IEnumerator Interact()
    {
        foreach (var gate in _gates)
        {
            if (gate.IsAnimationPlaying)
                yield break;          
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_animator.IsInTransition(0))
        {
            _animator.SetBool("isOn", !_animator.GetBool("isOn"));
            foreach (var gate in _gates)
            {                
                StartCoroutine(gate.Activate());
            }
        }        
        yield return null;
    }
}
