using UnityEngine;

public class RabbitBehaviorScript : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D _bunny;

    private bool _isGrounded = true;

    // Use this for initialization
    void Start()
    {
        _bunny = GetComponent<Rigidbody2D>();
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
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value > 0)
            sr.flipX = false;
        else if (value < 0)
            sr.flipX = true;

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = _bunny.velocity;
            vel.x = value * Speed;
            _bunny.velocity = vel;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("ground"))
        {
            //_isGrounded = false;
        }
    }
}