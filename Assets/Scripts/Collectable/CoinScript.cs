namespace Collectable
{
	public class CoinScript : Collectable {
	
		protected override void OnRabitHit (RabbitBehaviorScript rabit)
		{
			LevelController.Current.AddCoins (1);
			CollectedHide ();
		}
	
	}
}
