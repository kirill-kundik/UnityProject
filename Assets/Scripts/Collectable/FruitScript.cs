namespace Collectable
{
	public class FruitScript : Collectable {
	
		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddFruits (1);
			CollectedHide ();
		}
	
	}
}
