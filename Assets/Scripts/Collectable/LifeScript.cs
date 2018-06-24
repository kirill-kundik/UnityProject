namespace Collectable
{
	public class LifeScript : Collectable {
	
		protected override void OnRabitHit (Rabbit rabit)
		{
			if (LevelController.Current.LifesCounter >= 3)
			{
				return;
			}
			LevelController.Current.AddLifes (1);
			CollectedHide ();
		}
	
	}
}
