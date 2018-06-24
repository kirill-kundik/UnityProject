using System;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public static Rabbit LastRabbit;

    public AudioClip WalkingSound;
    public AudioClip DeathSound;
    public AudioClip LandingSound;
    
    public float Speed = 1.0f;
    public float MaxJumpTime = 1f;
    public float JumpSpeed = 3f;
    [HideInInspector] public bool Buffed;
    [HideInInspector] public bool Dead;
    [HideInInspector] public float InvulnerableTimeLeft;

    private Rigidbody2D _bunny;
    private bool _isGrounded;
    private float _jumpTime;
    private int _groundLayerId;
    private Vector3 _startingPosition;
    private Vector3 _defaultScale;
    private Animator _animator;
    
    private AudioSource _landSoundSource;
    private AudioSource _runSoundSource;
    private AudioSource _deathSoundSource;
    
    private Rabbit()
    {
    }

    private void Start()
    {
        _bunny = GetComponent<Rigidbody2D>();
        _groundLayerId = 1 << LayerMask.NameToLayer("Ground");
        _startingPosition = transform.position;
        _defaultScale = transform.localScale;
        _animator = GetComponent<Animator>();
        LastRabbit = this;
        
        _landSoundSource = gameObject.AddComponent<AudioSource>();
        _runSoundSource = gameObject.AddComponent<AudioSource>();
        _deathSoundSource = gameObject.AddComponent<AudioSource>();
        _runSoundSource.clip = WalkingSound;
        _landSoundSource.clip = LandingSound;
        _deathSoundSource.clip = DeathSound;
    }

    private void Update()
    {
        
        if (SoundManager.Instance.SoundOn && Time.timeScale > 0 && Math.Abs(_bunny.velocity.x) > 0.1f && _isGrounded)
        {
            if (!_runSoundSource.isPlaying)
                _runSoundSource.Play();
        }
        else
        {
            _runSoundSource.Pause();
        }
        
        if (InvulnerableTimeLeft > 0)
        {
            InvulnerableTimeLeft -= Time.deltaTime;
            _animator.SetBool("invincible", true);
        }
        else
        {
            _animator.SetBool("invincible", false);
        }
    }

    private void FixedUpdate()
    {
        var xAxisValue = Input.GetAxis("Horizontal");
        var yAxisValue = Input.GetAxis("Vertical");
        ControlMovement(xAxisValue, yAxisValue);
        ControlAnimations(xAxisValue, yAxisValue);
    }


    private void ControllAirTime(RaycastHit2D hit)
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _jumpTime = MaxJumpTime;
        }

        if (_jumpTime > 0)
        {
            if (Input.GetButton("Jump"))
            {
                _jumpTime -= Time.deltaTime;
                var vel = _bunny.velocity;
                vel.y = JumpSpeed * (_jumpTime / MaxJumpTime);
                _bunny.velocity = vel;
            }
            else
            {
                _jumpTime = 0;
            }
        }
        
        if (SoundManager.Instance.SoundOn && _isGrounded && !_landSoundSource.isPlaying)
            _landSoundSource.Play();
    }

    private void ControlTakeOffAndLanding()
    {
        var from = transform.position + Vector3.up * 0.3f;
        var to = transform.position + Vector3.down * 0.1f;
        var hit = Physics2D.Linecast(from, to, _groundLayerId);
        _isGrounded = hit;
        ControllAirTime(hit);
    }

    private void ControlMovement(float xAxisValue, float yAxisValue)
    {
        if (Dead) return;
        var render = GetComponent<SpriteRenderer>();
        render.flipX = xAxisValue < 0;

        var vel = _bunny.velocity;
        vel.x = xAxisValue * Speed;
        _bunny.velocity = vel;
        ControlTakeOffAndLanding();
    }


    private void ControlAnimations(float xAxisValue, float yAxisValue)
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("run", Mathf.Abs(xAxisValue) > 0);
        animator.SetBool("jump", !_isGrounded);
    }

    public void Revive()
    {
        var animator = GetComponent<Animator>();
        animator.SetTrigger("reset");
        Debuff();
        transform.position = _startingPosition;
        Dead = false;
    }

    public void Death()
    {
        var animator = GetComponent<Animator>();
        Dead = true;
        _bunny.velocity = Vector2.zero;
        animator.SetTrigger("death");
        _deathSoundSource.Play();
        InvulnerableTimeLeft = 0;
    }

    public void AcceptBuff()
    {
        if (Buffed)
        {
            return;
        }

        Buffed = true;
        transform.localScale = _defaultScale * 2;
    }

    public void GotDamaged()
    {
        if (Dead || InvulnerableTimeLeft > 0)
            return;
        if (Buffed)
        {
            Debuff();
            return;
        }

        LevelController.Current.OnRabbitDeath(this);
    }


    public void Debuff()
    {
        Buffed = false;
        transform.localScale = _defaultScale;
        InvulnerableTimeLeft = 4;
    }

    public void SmallJump()
    {
        var vel = _bunny.velocity;
        vel.y = 5;
        _bunny.velocity = vel;
    }
}