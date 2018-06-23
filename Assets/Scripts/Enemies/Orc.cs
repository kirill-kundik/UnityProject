using UnityEngine;

namespace Enemies
{
    public class Orc : MonoBehaviour
    {
        public float Speed = 3;
        public Vector3 PointBOffset;

        protected Mode OrkMode = Mode.GoToA;
        protected Rigidbody2D Body;
        protected Animator OrkAnimator;

        private Vector3 _pointA;
        private Vector3 _pointB;


        protected float Direction
        {
            get
            {
                if (OrkMode == Mode.GoToRabbit)
                    return transform.position.x - Rabbit.LastRabbit.transform.position.x > 0.0f ? -1 : 1;
                return transform.position.x - (OrkMode == Mode.GoToA ? _pointA : _pointB).x > 0.0f ? -1 : 1;
            }
        }


        public enum Mode
        {
            GoToA,
            GoToB,
            GoToRabbit,
            Atak,
            Dead
        }

        // Use this for initialization
        private void Start()
        {
            _pointA = transform.position;
            _pointB = transform.position + PointBOffset;
            Body = GetComponent<Rigidbody2D>();
            OrkAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            UpdateMode();
            Move();
        }


        private void UpdateMode()
        {
            if (OrkMode == Mode.Atak || OrkMode == Mode.Dead)
                return;

            if (RabbitEntered())
            {
                OrkMode = Mode.GoToRabbit;
                OnRabbitEntered();
            }
            else if (OrkMode == Mode.GoToRabbit)
            {
                OrkMode = Mode.GoToA;
            }

            if (OrkMode == Mode.GoToA)
            {
                if (IsArrived(_pointA))
                {
                    OrkMode = Mode.GoToB;
                }
            }

            if (OrkMode == Mode.GoToB)
            {
                if (IsArrived(_pointB))
                {
                    OrkMode = Mode.GoToA;
                }
            }
        }

        private bool IsArrived(Vector3 dest)
        {
            return Mathf.Abs(transform.position.x - dest.x) < 0.5f;
        }

        private void Move()
        {
            if (OrkMode == Mode.Atak || OrkMode == Mode.Dead) return;
            var render = GetComponent<SpriteRenderer>();
            render.flipX = Direction > 0;

            var vel = Body.velocity;
            vel.x = Direction * Speed;
            Body.velocity = vel;

            OrkAnimator.SetBool("run", true);
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
                if (OrkMode == Mode.Dead) return;
                var orkCol = GetComponent<BoxCollider2D>();
                if (rabbit.transform.position.y > transform.position.y + orkCol.size.y / 2 + orkCol.offset.y - 0.2f)
                {
                    OrkDeath();
                    rabbit.SmallJump();
                    return;
                }

                if (OrkMode == Mode.Atak)
                    return;
                OrkAnimator.SetTrigger("attack_hit");
                LevelController.Current.OnRabbitDeath(rabbit);
                OrkMode = Mode.Atak;
            }
        }

        private void OrkDeath()
        {
            OrkMode = Mode.Dead;
            OrkAnimator.SetTrigger("die");
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnCollisionEnter2D(other);
        }

        public void OnAttackAnimationEnd()
        {
            OrkMode = Mode.GoToA;
        }

        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}