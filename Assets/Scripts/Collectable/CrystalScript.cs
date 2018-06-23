namespace Collectable
{
    public class CrystalScript : Collectable
    {
        
        protected override void OnRabitHit (RabbitBehaviorScript rabit)
        {
            LevelController.Current.AddCrystal(1);
            CollectedHide ();
        }
        
    }
}