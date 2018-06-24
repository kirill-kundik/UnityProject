using UnityEngine;

namespace Enemies
{
    public class Orc : MonoBehaviour
    {
        public float Speed = 3;
        public Vector3 PointBOffset;

        public AudioClip AudioAttack;
        public AudioClip AudioDie;
        
        protected Mode OrcMode = Mode.GoToA;
        protected Rigidbody2D Body;
        protected Animator OrcAnimator;

        private Vector3 _pointA;
        private Vector3 _pointB;
        
        protected AudioSource SoundSource;
        
        protected float Direction
        {
            get
            {
                if (OrcMode == Mode.GoToRabbit || OrcMode == Mode.Attack)
                    return transform.position.x - Rabbit.LastRabbit.transform.position.x > 0.0f ? -1 : 1;
                return transform.position.x - (OrcMode == Mode.GoToA ? _pointA : _pointB).x > 0.0f ? -1 : 1;
            }
        }


        public enum Mode
        {
            GoToA,
            GoToB,
            GoToRabbit,
            Attack,
            Dead
        }

        // Use this for initialization
        private void Start()
        {
            _pointA = transform.position;
            _pointB = transform.position + PointBOffset;
            Body = GetComponent<Rigidbody2D>();
            OrcAnimator = GetComponent<Animator>();
            
            SoundSource = gameObject.AddComponent<AudioSource>();
            SoundSource.clip = AudioAttack;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            UpdateMode();
            Move();
        }


        private void UpdateMode()
        {
            if (OrcMode == Mode.Attack || OrcMode == Mode.Dead)
                return;

            if (RabbitEntered())
            {
                OrcMode = Mode.GoToRabbit;
                OnRabbitEntered();
            }
            else if (OrcMode == Mode.GoToRabbit)
            {
                OrcMode = Mode.GoToA;
            }

            if (OrcMode == Mode.GoToA)
            {
                if (IsArrived(_pointA))
                {
                    OrcMode = Mode.GoToB;
                }
            }

            if (OrcMode == Mode.GoToB)
            {
                if (IsArrived(_pointB))
                {
                    OrcMode = Mode.GoToA;
                }
            }
        }

        private bool IsArrived(Vector3 dest)
        {
            return Mathf.Abs(transform.position.x - dest.x) < 0.5f;
        }

        private void Move()
        {
            if (OrcMode == Mode.Attack || OrcMode == Mode.Dead) return;
            var render = GetComponent<SpriteRenderer>();
            render.flipX = Direction > 0;

            var vel = Body.velocity;
            vel.x = Direction * Speed;
            Body.velocity = vel;

            OrcAnimator.SetBool("run", true);
        }

        private bool RabbitEntered()
        {
            var rabbitPosX = Rabbit.LastRabbit.transform.position.x;
            return rabbitPosX > Mathf.Min(_pointA.x, _pointB.x)
                   && rabbitPosX < Mathf.Max(_pointA.x, _pointB.x);
        }

        protected virtual void OnRabbitEntered()
        {
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var rabbit = other.transform.GetComponent<Rabbit>();
            if (rabbit)
            {
                if (OrcMode == Mode.Dead) return;
                var orcCol = GetComponent<BoxCollider2D>();
                if (rabbit.transform.position.y > transform.position.y + orcCol.size.y / 2 + orcCol.offset.y - 0.4f)
                {
                    OrcDeath();
                    rabbit.SmallJump();
                    return;
                }

                if (OrcMode == Mode.Attack)
                    return;
                OrcAnimator.SetTrigger("attack_hit");
                
                SoundManager.Instance.PlaySound(AudioAttack, SoundSource);
                rabbit.GotDamaged();
                OrcMode = Mode.Attack;
            }
        }

        private void OrcDeath()
        {
            OrcMode = Mode.Dead;
            OrcAnimator.SetTrigger("die");
            
            SoundSource = gameObject.AddComponent<AudioSource>();
            SoundSource.clip = AudioAttack;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnCollisionEnter2D(other);
        }

        public void OnAttackAnimationEnd()
        {
            OrcMode = Mode.GoToA;
        }

        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}