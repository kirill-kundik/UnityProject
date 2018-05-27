using UnityEngine;

public class LevelController : MonoBehaviour
{

	public static LevelController Current;
	public int LifesCounter = 3;

	private Vector3 _startingPosition;

	void Awake()
	{
		Current = this;
	}

	public int GetLifesCounter()
	{
		return LifesCounter;
	}

	public void SetStartPosition(Vector3 pos) {
		_startingPosition = pos;
	}
	
	public void OnRabbitDeath(RabbitBehaviorScript rabbit)
	{
		if (LifesCounter > 0)
		{
			LifesCounter--;
		}
		else
		{
			// Game Over
		}

		rabbit.transform.position = _startingPosition;
	}
}
