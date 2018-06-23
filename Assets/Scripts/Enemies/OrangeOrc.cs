using UnityEngine;

namespace Enemies
{
	public class OrangeOrc : Orc {

		public float CooldownTime = 2f;
		public float CarrotVelocity = 0.1f;
		public float CarrotLifetime = 5f;
		private float _lastThrowTime;
		public GameObject PrefabCarrot;

		private void AttackWithCarrot()
		{
			var render = GetComponent<SpriteRenderer>();
			render.flipX = Direction > 0;
			_lastThrowTime = Time.time;
			OrcAnimator.SetTrigger("attack_throw");
			Body.velocity = Vector2.zero;
			OrcMode = Mode.Attack;
			Debug.Log(Direction);

			LaunchCarrot();
		}

		protected override void OnRabbitEntered()
		{
			if (Time.time - _lastThrowTime < CooldownTime)
			{
				return;
			}

			AttackWithCarrot();
		}

		private void LaunchCarrot()
		{
			var carrot = Instantiate(PrefabCarrot).GetComponent<Carrot>();
			carrot.transform.position = transform.position + Vector3.up * 0.6f;
			carrot.Init(CarrotVelocity, Direction, CarrotLifetime);
		}
	}
}
