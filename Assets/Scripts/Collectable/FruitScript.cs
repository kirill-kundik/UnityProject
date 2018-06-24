namespace Collectable
{
	public class FruitScript : Collectable
	{
		private static int _freeId;
		private int _id;

		private void Start()
		{
			_id = _freeId++;
		}

		protected override void OnRabitHit (Rabbit rabit)
		{
			LevelController.Current.AddFruits (_id);
			CollectedHide ();
		}
	
	}
}
