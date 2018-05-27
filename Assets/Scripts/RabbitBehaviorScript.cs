using UnityEditor;
using UnityEngine;

public class RabbitBehaviorScript : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D _bunny;
    private Animator _animator;
    
    private bool _isGrounded = true;

    private bool _jumpActive;
    private float _jumpTime;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    
    // Use this for initialization
    void Start()
    {
        _bunny = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        LevelController.Current.SetStartPosition(transform.position);
        LevelController.Current.SetStartRotation(transform.rotation);
    }

    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layerId = 1 << LayerMask.NameToLayer ("Ground");
//Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layerId);
        if(hit) {
            _isGrounded = true;
        } else {
            _isGrounded = false;
        }
//Намалювати лінію (для розробника)
        Debug.DrawLine (from, to, Color.red);
        
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded) {
            _jumpActive = true;
        }
        if(_jumpActive) {
//Якщо кнопку ще тримають
            if(Input.GetKey(KeyCode.Space)) {
                _jumpTime += Time.deltaTime;
                if (_jumpTime < MaxJumpTime) {
                    Vector2 vel = _bunny.velocity;
                    vel.y = JumpSpeed * (1.0f - _jumpTime / MaxJumpTime);
                    _bunny.velocity = vel;
                }
            } else {
                _jumpActive = false;
                _jumpTime = 0;
            }
        }
        
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value > 0)
            sr.flipX = false;
        else if (value < 0)
            sr.flipX = true;

        if (Mathf.Abs(value) > 0)
        {
            _animator.SetBool("run", true);
            
            Vector2 vel = _bunny.velocity;
            vel.x = value * Speed;
            _bunny.velocity = vel;
        }
        else
        {
            _animator.SetBool("run", false);
        }

        if (_isGrounded)
        {
            _animator.SetBool("jump", false);
        }
        else
        {
            _animator.SetBool("jump", true);
        }
        
    }

}