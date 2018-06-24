namespace Collectable
{
	public class FruitScript : Collectable
	{
		private static int _freeId;
		public int Id { get; private set; }

		private void Awake()
		{
			Id = _freeId++;
		}

		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddFruits (Id);
			CollectedHide ();
		}
	
	}
}
