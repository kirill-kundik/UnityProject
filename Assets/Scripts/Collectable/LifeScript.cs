namespace Collectable
{
	public class LifeScript : Collectable {
	
		protected override void OnRabitHit (RabbitBehaviorScript rabit)
		{
			LevelController.Current.AddLifes (1);
			CollectedHide ();
		}
	
	}
}
