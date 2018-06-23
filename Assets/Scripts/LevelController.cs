using UnityEngine;

public class LevelController : MonoBehaviour
{

	public static LevelController Current;
	public int LifesCounter = 3;
	public int CoinCounter = 0;
	public int CrystalCounter = 0;
	public int MushroomCounter = 0;
	public int FruitCounter = 0;

	private Vector3 _startingPosition;
	private Quaternion _startingRotation;
	
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

	public void SetStartRotation(Quaternion rotation)
	{
		_startingRotation = rotation;
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
		rabbit.transform.rotation = _startingRotation;
	}

	public void AddCoins(int count)
	{
		CoinCounter += count;
	}

	public void AddCrystal(int count)
	{
		CrystalCounter += count;
	}

	public void AddMushrooms(int count)
	{
		MushroomCounter += count;
	}

	public void AddFruits(int count)
	{
		FruitCounter += count;
	}

	public void AddLifes(int count)
	{
		LifesCounter += count;
	}
}
