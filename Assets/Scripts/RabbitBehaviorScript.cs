using UnityEngine;

public class RabbitBehaviorScript : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D _bunny;
    private Animator _animator;
    
    private bool _isGrounded = true;

    // Use this for initialization
    void Start()
    {
        _bunny = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Vector2 vel = _bunny.velocity;
            vel.y = Speed;
            _bunny.velocity = vel;
            
            _isGrounded = false;
            _animator.SetBool("jump", true);
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("ground"))
        {
            _isGrounded = true;
            _animator.SetBool("jump", false);
        }
    }

}