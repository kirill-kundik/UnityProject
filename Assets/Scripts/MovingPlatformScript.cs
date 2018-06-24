using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{

	public Vector3 MoveBy;
	public float Speed = 0.5f;
	public float TimeToWait = 1f;
	
	private Vector3 _pointA;
	private Vector3 _pointB;
	private bool _isGoingToA;

	private float _timeOut;
	
	// Use this for initialization
	void Start ()
	{
		_pointA = transform.position;
		_pointB = _pointA + MoveBy;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 myPos = transform.position;
		Vector3 target = _isGoingToA ? _pointA : _pointB;

		if (IsArrived(myPos, target))
		{
			_timeOut = TimeToWait;
			_isGoingToA = !_isGoingToA;
		}
		else
		{
			if (_timeOut > 0)
			{
				_timeOut -= Time.deltaTime;
			}
			else
			{
				myPos = Vector3.MoveTowards(myPos, target, Speed);
			}
		}

		transform.position = myPos;
	}

	bool IsArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			other.transform.parent = transform;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			other.transform.parent = null;
		}
	}
}
