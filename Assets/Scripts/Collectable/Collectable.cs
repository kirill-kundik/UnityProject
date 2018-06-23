using UnityEngine;

namespace Collectable
{
	public class Collectable : MonoBehaviour {
	
		private bool _hideAnimation;
	
		protected virtual void OnRabitHit(RabbitBehaviorScript rabbit) {
		}
	
		void OnTriggerEnter2D(Collider2D other) {
			if(!_hideAnimation) {
				RabbitBehaviorScript rabit = other.GetComponent<RabbitBehaviorScript>();
				if(rabit != null) {
					OnRabitHit (rabit);
				}
			}
		}
		public void CollectedHide() {
			_hideAnimation = true;
			Destroy(gameObject);
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			OnTriggerEnter2D(other);
		}
	}
}
