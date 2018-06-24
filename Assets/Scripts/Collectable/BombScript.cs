using UnityEngine;

namespace Collectable
{
	public class BombScript : Collectable
	{

		protected override void OnRabitHit (Rabbit rabit)
		{
			rabit.GotDamaged();
			CollectedHide ();
		}
	}
}
