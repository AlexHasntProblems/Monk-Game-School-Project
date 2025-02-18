using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chest : MonoBehaviour, IInteractive
{
    [SerializeField] private Item _item;
    private uint _itemsCount;
    private Animator _animator;
    private Hint _hint;
    private AudioSource _audioSource;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _hint = GameObject.Find("Canvas").transform.Find("Hint").GetComponent<Hint>();
        SpawnLoot();
    }
    public IEnumerator Interact()
    {
        if (_animator.GetBool("isOpened") == false)
        {
            AudioTools.PlaySound(_audioSource);
            _animator.SetBool("isOpened", true);
            _item.Apply(_itemsCount);
            _hint.ShowHintWithText("+ " + _itemsCount + " " + _item.names[(int)GlobalSettings.language]);
            yield return new WaitForSeconds(3f);
            _hint.Hide();
        }     
    }

    private void SpawnLoot()
    {
        _itemsCount = Convert.ToUInt32(UnityEngine.Random.Range(Convert.ToInt32(_item.MinSpawnCount), Convert.ToInt32(_item.MaxSpawnCount)));
    }

    public void ChangeLootCount(uint count)
    {
        _itemsCount = count;
    } 
}
