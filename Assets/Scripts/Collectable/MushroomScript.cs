namespace Collectable
{
    public class MushroomScript : Collectable {
	
        protected override void OnRabitHit (RabbitBehaviorScript rabit)
        {
            LevelController.Current.AddMushrooms (1);
            CollectedHide ();
        }
	
    }
}
