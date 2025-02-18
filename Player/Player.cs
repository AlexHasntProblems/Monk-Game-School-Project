using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _healthPoint;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Transform _projectileOrigin;
    [SerializeField] private float _interactionRadius;
    public AudioClip soundOfWalkingOnConcrete;
    public AudioClip soundOfWalkingOnGrass;
    public AudioClip soundOfGettingDamage;
    public AudioClip soundOfJumping;
    private static Player _instance;
    private uint _bladesCount;
    private AudioSource _audioSource;
    public static Player Instance { private set {} get { return _instance; } }

    public float HealthPoint { private set; get; }
    public uint BladesCount 
    { 
        set 
        { 
            if (value < 0) 
                _bladesCount = 0; 
            else 
                _bladesCount = value;
            BladeCounter.Instance.UpdateCounter();
        } 
        get 
        { 
            return _bladesCount; 
        } 
    }
    public float JumpForce { private set {} get { return _jumpForce; } }
    public float MovementSpeed { set { if (value <= 0f) _movementSpeed = 0f; else _movementSpeed = value; } get { return _movementSpeed; } }
    public GameObject Projectile { private set {} get { return _projectile; } }
    public float AttackDelay { private set {} get { return _attackDelay; } }
    public Transform ProjectileOrigin { private set {} get { return _projectileOrigin; } }
    public float InteractionRadius { private set {} get { return _interactionRadius; } }

    private void Awake()
    {
        _instance = this;
        HealthPoint = _healthPoint;
        _audioSource = GetComponent<AudioSource>();
    }

    public void ApplyDamage(float damage)
    {
        AudioTools.PlaySound(_audioSource, soundOfGettingDamage);
        HealthPoint -= damage;
        HealthBar.Instance.UpdateHealthBar();
        if (HealthPoint <= 0f)
            Destroy(gameObject);      
    }

    public void Heal(float healCount)
    {       
        HealthPoint += healCount;
        if (HealthPoint > _healthPoint)
            HealthPoint = _healthPoint; 
        HealthBar.Instance.UpdateHealthBar();
    }

    private void OnDestroy()
    {
        GameObject.Find("Canvas")?.transform.Find("Death Screen")?.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
