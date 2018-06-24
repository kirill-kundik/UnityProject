using UnityEngine;

namespace Collectable
{
	public class Collectable : MonoBehaviour
	{

		public AudioClip AudioClip;
		
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
			SoundManager.Instance.PlaySound(AudioClip);
			_hideAnimation = true;
			Destroy(gameObject);
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			OnTriggerEnter2D(other);
		}
	}
}
