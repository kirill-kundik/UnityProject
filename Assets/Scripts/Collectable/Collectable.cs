using UnityEngine;

namespace Collectable
{
	public class Collectable : MonoBehaviour {
	
		private bool _hideAnimation;
	
		protected virtual void OnRabitHit(Rabbit rabbit) {
		}
	
		void OnTriggerEnter2D(Collider2D other) {
			if(!_hideAnimation) {
				Rabbit rabit = other.GetComponent<Rabbit>();
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
