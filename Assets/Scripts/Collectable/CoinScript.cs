namespace Collectable
{
	public class CoinScript : Collectable {
	
		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddCoins (1);
			CollectedHide ();
		}
	
	}
}
