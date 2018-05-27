using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

	public static LevelController Current;
	public int LifesCounter = 3;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake()
	{
		Current = this;
	}

	int GetLifesCounter()
	{
		return LifesCounter;
	}
}
