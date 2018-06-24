using System.Collections.Generic;
using Collectable;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;

    public int LifesCounter = 3;
    private LevelStat _levelStat;
    private int _maxFruits = 11;

    public UiInGameController UiController;

    private List<CrystalScript.CrystalType> _collectedCrystals = new List<CrystalScript.CrystalType>();

    private bool _pause;

    public bool Pause
    {
        get { return _pause; }
        set
        {
            _pause = value;
            Time.timeScale = _pause ? 0 : 1;
        }
    }

    void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        string str = PlayerPrefs.GetString (SceneManager.GetActiveScene().name, null);
        _levelStat = JsonUtility.FromJson<LevelStat> (str);
        if(_levelStat == null) {
            _levelStat = new LevelStat ();
        }
        _maxFruits = FindObjectsOfType<FruitScript>().Length;
        UiController.SetCoins(PlayerPrefs.GetInt("coins", 0));
        UiController.SetFruits(_levelStat.CollectedFruits.Count, _maxFruits);
    }

    public int GetLifesCounter()
    {
        return LifesCounter;
    }

    public void OnRabbitDeath(Rabbit rabbit)
    {
        if (LifesCounter > 0)
        {
            LifesCounter--;
            rabbit.Death();
            UiController.SetLifes(LifesCounter);
        }
        else
        {
        }
    }

    public void AddCoins(int count)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + count);
        PlayerPrefs.Save();
        UiController.SetCoins(PlayerPrefs.GetInt("coins"));
    }

    public void AddCrystal(CrystalScript.CrystalType type)
    {
        if (_collectedCrystals.IndexOf(type) >= 0)
            return;
        _collectedCrystals.Add(type);
        UiController.SetCrystal(type);
        if (_collectedCrystals.Count >= 3)
            _levelStat.HasCrystals = true;
    }

    public void AddFruits(int id)
    {
       if( _levelStat.CollectedFruits.IndexOf(id) >= 0)
            return;
        _levelStat.CollectedFruits.Add(id);
        if (_levelStat.CollectedFruits.Count >= _maxFruits)
            _levelStat.HasAllFruits = true;
        UiController.SetFruits(_levelStat.CollectedFruits.Count, _maxFruits);
    }

    public void AddLifes(int count)
    {
        LifesCounter += count;
    }

    public void Save()
    {
        string str = JsonUtility.ToJson(_levelStat);
        PlayerPrefs.SetString (SceneManager.GetActiveScene().name, str);
    }
}