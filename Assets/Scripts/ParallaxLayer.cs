using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{

	public float SlowDown = 0.5f;

	private Vector3 _lastPosition;

	void Awake()
	{
		_lastPosition = Camera.main.transform.position;
	}

	void LateUpdate()
	{

		Vector3 newPosition = Camera.main.transform.position;
		Vector3 diff = newPosition - _lastPosition;

		_lastPosition = newPosition;

		Vector3 myPosition = transform.position;
		myPosition += SlowDown * diff;

		transform.position = myPosition;

	}
}
