namespace Collectable
{
    public class CrystalScript : Collectable
    {
        
        protected override void OnRabitHit (Rabbit rabit)
        {
            LevelController.Current.AddCrystal(1);
            CollectedHide ();
        }
        
    }
}