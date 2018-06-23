using UnityEngine;

public class DeathHere : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		Rabbit rabbit = other.GetComponent<Rabbit>();

		if (rabbit != null)
		{
			LevelController.Current.OnRabbitDeath(rabbit);
		}
	}
	
}
