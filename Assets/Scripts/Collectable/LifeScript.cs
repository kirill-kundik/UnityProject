namespace Collectable
{
	public class LifeScript : Collectable {
	
		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddLifes (1);
			CollectedHide ();
		}
	
	}
}
