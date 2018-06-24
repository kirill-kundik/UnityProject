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
        if (!_locked)
            SceneManager.LoadScene("Level" + LevelToBeLoaded);
    }

    private void Start()
    {
        string str = PlayerPrefs.GetString("Level" + LevelToBeLoaded, null);
        LevelStat curLevel = JsonUtility.FromJson<LevelStat>(str) ?? new LevelStat();

        if (LevelToBeLoaded == 1)
        {
            _locked = false;
        }
        else
        {
            str = PlayerPrefs.GetString("Level" + (LevelToBeLoaded - 1), null);
            LevelStat prevLevel = JsonUtility.FromJson<LevelStat>(str) ?? new LevelStat();
            _locked = !prevLevel.LevelPassed;
        }

        transform.Find("Fruit").GetComponent<SpriteRenderer>().enabled = curLevel.HasAllFruits;
        transform.Find("Crystal").GetComponent<SpriteRenderer>().enabled = curLevel.HasCrystals;
        transform.Find("Completed").GetComponent<SpriteRenderer>().enabled = curLevel.LevelPassed;
        transform.Find("Locked").GetComponent<SpriteRenderer>().enabled = _locked;

        transform.Find("DoorNumber").GetComponent<SpriteRenderer>().sprite = Components[LevelToBeLoaded - 1];
    }
}