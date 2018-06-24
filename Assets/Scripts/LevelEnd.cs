﻿using UnityEngine;

public class LevelEnd : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Rabbit>() != null)
			LevelController.Current.OnLevelCompleted();
	}
}
