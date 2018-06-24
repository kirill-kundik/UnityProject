using System;
using System.Collections.Generic;
using Collectable;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;

    public int LifesCounter = 3;
    private LevelStat _levelStat;
    private int _maxFruits = 11;

    public UiInGameController UiController;
    public Animator LoseGameWindow;
    public Animator WinGameWindow;

    private List<CrystalScript.CrystalType> _collectedCrystals = new List<CrystalScript.CrystalType>();

    private bool _pause;

    private int _coinsCollected;
    
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
        if (Current != null && Current != this)
        {
            Destroy(this);
            return;
        }

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
        
        DisplayFruits();
    }
    
    private void DisplayFruits()
    {
        var freeId = 0;
        var fruits = FindObjectsOfType<FruitScript>();
        Array.ForEach(fruits, fruit => fruit.Id = freeId++);
        var updatedIDs = new List<int>();
        foreach (var id in _levelStat.CollectedFruits)
        {
            var item = Array.Find(fruits, fruit => fruit.Id == id);
            if (item == null) continue;
            updatedIDs.Add(item.Id);
            item.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }

        _levelStat.CollectedFruits = updatedIDs;

        _maxFruits = fruits.Length;
        UiController.SetFruits(_levelStat.CollectedFruits.Count, _maxFruits);
    }

    
    public int GetLifesCounter()
    {
        return LifesCounter;
    }

    public void OnRabbitDeath(Rabbit rabbit)
    {
        if (LifesCounter > 1)
        {
            LifesCounter--;
            rabbit.Death();
            UiController.SetLifes(LifesCounter);
        }
        else
        {
            SetUpCollectedDiamonds(LoseGameWindow.transform.Find("Panel"));
            LoseGameWindow.SetTrigger("open");
            Pause = true;
        }
    }

    public void OnLevelCompleted()
    {
        _levelStat.LevelPassed = true;
        Save();

        var panel = WinGameWindow.transform.Find("Panel");
        panel.Find("TextFruits").GetComponent<Text>().text = _levelStat.CollectedFruits.Count + "/" + _maxFruits;
        panel.Find("TextCoins").GetComponent<Text>().text = "+" + _coinsCollected;
        SetUpCollectedDiamonds(panel);

        WinGameWindow.SetTrigger("open");
        Pause = true;
    }
    
    private void SetUpCollectedDiamonds(Transform windowPanel)
    {
        windowPanel.Find("CrystalBlue").GetComponent<Image>().sprite = _collectedCrystals.Contains(CrystalScript.CrystalType.Blue)
            ? UiController.CrystalBlue
            : UiController.CrystalEmpty;
        windowPanel.Find("CrystalGreen").GetComponent<Image>().sprite =
            _collectedCrystals.Contains(CrystalScript.CrystalType.Green)
                ? UiController.CrystalGreen
                : UiController.CrystalEmpty;
        windowPanel.Find("CrystalRed").GetComponent<Image>().sprite = _collectedCrystals.Contains(CrystalScript.CrystalType.Red)
            ? UiController.CrystalRed
            : UiController.CrystalEmpty;
    }
    
    public void AddCoins(int count)
    {
        _coinsCollected += count;
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
       if( _levelStat.CollectedFruits.Contains(id))
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
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("LevelChooser");
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}