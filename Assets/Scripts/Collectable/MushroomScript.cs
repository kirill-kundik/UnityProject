﻿namespace Collectable
{
    public class MushroomScript : Collectable {
	
        protected override void OnRabitHit (Rabbit rabit)
        {
            rabit.AcceptBuff();
            CollectedHide ();
        }
	
    }
}
