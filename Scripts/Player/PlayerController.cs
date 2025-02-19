using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private InputControls _inputControls;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private bool _isGrounded;
    private float _attackTimer;
    private float _direction;

    public bool IsGrounded { private set {} get { return _isGrounded; } }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _inputControls = new InputControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _inputControls.Player.Jump.performed += context => Jump();
        _inputControls.Player.Interact.performed += context => Interact();
        _inputControls.Player.Throwblade.performed += context => ThrowBlade();
        _inputControls.UI.Pause.performed += context => Pause();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }

    private void Update()
    {
        UpdateAttackTimer();
    }
    private void FixedUpdate()
    {
        GroundDetector groundDetector = transform.GetChild(1).GetComponent<GroundDetector>();
        _isGrounded = groundDetector.IsGrounded;
        if (!UnityEngine.Device.Application.isMobilePlatform)
            _direction = _inputControls.Player.Move.ReadValue<float>();
        Move(_direction);
        if (_rigidbody.velocity == Vector2.zero && IsGrounded)
            _animator.SetTrigger("idle");
        else if (!IsGrounded)
            _animator.SetTrigger("jumping");
        else if (IsGrounded && _rigidbody.velocity != Vector2.zero)
        {
            if (groundDetector.SurfaceType == Surface.Grass)
                AudioTools.PlaySound(_audioSource, Player.Instance.soundOfWalkingOnGrass);
            else if (groundDetector.SurfaceType == Surface.Concrete)
                AudioTools.PlaySound(_audioSource, Player.Instance.soundOfWalkingOnConcrete);
            _animator.SetTrigger("walk");
        }
            
    }

    public void Jump()
    {
        if (IsGrounded)
        {       
            _rigidbody.AddForce(new Vector2(0, 1f) * _player.JumpForce);
            _audioSource.Stop();
            AudioTools.PlaySound(_audioSource, Player.Instance.soundOfJumping);
        }            
    }
    public void Interact()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), _player.InteractionRadius))
        {
            if (collider.gameObject.tag == "Interactive")
            {
                StartCoroutine(collider.gameObject.GetComponent<IInteractive>().Interact());
            }     
        }
    }   

    private void Move(float direction)
    {
        
        _rigidbody.velocity = new Vector2(direction * Time.fixedDeltaTime * _player.MovementSpeed, _rigidbody.velocity.y);
        if (direction > 0f)
        {
            _spriteRenderer.flipX = false;
        }           
        else if (direction < 0f)
        {
             _spriteRenderer.flipX = true;
        }        
    }
    public void OnLeftButtonDown()
    {
        _direction = -1f;
    }

    public void OnRightButtonDown()
    {
        _direction = 1f;
    }
    public void OnMovementButtonUp()
    {
        _direction = 0f;
    }
    public void ThrowBlade()
    {       
        if (_attackTimer <= 0f && _player.BladesCount > 0 && !Cursor.visible)
        {
           GameObject projectile = Instantiate(_player.Projectile, _player.ProjectileOrigin.position, Quaternion.Euler(0f, 0f, 0f));

            if (_spriteRenderer.flipX == false)
            {
                projectile.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                projectile.GetComponent<SpriteRenderer>().flipX = true;
            }
            _attackTimer = _player.AttackDelay;
            _player.BladesCount--;
        }       
    }

    private void UpdateAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
    }

    public void Pause()
    {
        GameObject window = GameObject.Find("Canvas").transform.Find("Settings Window").gameObject;
    
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }   
        else
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        window.SetActive(!window.activeSelf);
    }
}
