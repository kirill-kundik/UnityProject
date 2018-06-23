namespace Collectable
{
	public class FruitScript : Collectable {
	
		protected override void OnRabitHit (RabbitBehaviorScript rabit)
		{
			LevelController.Current.AddFruits (1);
			CollectedHide ();
		}
	
	}
}
