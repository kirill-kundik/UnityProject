using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public int LevelToBeLoaded;
	public Sprite[] Components;
	private bool _locked;

	private void OnTriggerEnter2D(Collider2D other)
	{
		SceneManager.LoadScene("Level"+LevelToBeLoaded);

	}

	private void Start()
	{
		transform.Find("DoorNumber").GetComponent<SpriteRenderer>().sprite = Components[LevelToBeLoaded - 1];
	}
}
