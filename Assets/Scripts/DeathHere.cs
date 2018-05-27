using UnityEngine;

public class DeathHere : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		RabbitBehaviorScript rabbit = other.GetComponent<RabbitBehaviorScript>();

		if (rabbit != null)
		{
			LevelController.Current.OnRabbitDeath(rabbit);
		}
	}
	
}
