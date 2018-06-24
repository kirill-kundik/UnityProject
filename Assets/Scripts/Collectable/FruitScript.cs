namespace Collectable
{
	public class FruitScript : Collectable
	{
		
		public int Id { get; set; }
		
		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddFruits (Id);
			CollectedHide ();
		}
	
	}
}
