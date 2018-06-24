namespace Collectable
{
    public class CrystalScript : Collectable
    {

        public enum CrystalType
        {
            Blue,Green, Red
        }

        public CrystalType Type = CrystalType.Blue;
        
        protected override void OnRabitHit (Rabbit rabit)
        {
            LevelController.Current.AddCrystal(Type);
            CollectedHide ();
        }
        
    }
}