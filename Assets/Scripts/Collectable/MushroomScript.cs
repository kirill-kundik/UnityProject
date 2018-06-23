namespace Collectable
{
    public class MushroomScript : Collectable {
	
        protected override void OnRabitHit (Rabbit rabit)
        {
            LevelController.Current.AddMushrooms (1);
            CollectedHide ();
        }
	
    }
}
