using System.Collections.Generic;
using Collectable;
using UI;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public int LifesCounter = 3;
    public int CoinCounter = 0;
    public int FruitCounter = 0;

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
        CoinCounter += count;
        UiController.SetCoins(CoinCounter);
    }

    public void AddCrystal(CrystalScript.CrystalType type)
    {
        if (_collectedCrystals.IndexOf(type) >= 0)
            return;
        _collectedCrystals.Add(type);
        UiController.SetCrystal(type);
        
    }

    public void AddFruits(int count)
    {
        FruitCounter += count;
        UiController.SetFruits(FruitCounter);
    }

    public void AddLifes(int count)
    {
        LifesCounter += count;
    }
}