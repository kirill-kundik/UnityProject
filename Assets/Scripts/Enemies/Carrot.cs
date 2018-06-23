using System.Collections;
using UnityEngine;

namespace Enemies
{
	public class Carrot : Collectable.Collectable {

		public float Velocity;
		public float Direction;
		public float Lifetime;

		public void Init(float velocity, float direction, float lifetime)
		{
			Velocity = velocity;
			Direction = direction;
			Lifetime = lifetime;
		}

		private void Start()
		{
			GetComponent<SpriteRenderer>().flipX = Direction < 0;
			StartCoroutine(DisappearLater());
		}

		private void Update()
		{
			transform.position += Vector3.right * Direction * Velocity;
		}

		protected override void OnRabitHit(Rabbit rabbit)
		{
			rabbit.GotDamaged();
			Destroy(gameObject);
		}

		private IEnumerator DisappearLater()
		{
			yield return new WaitForSeconds(Lifetime);
			Destroy(gameObject);
		}
	}
}
