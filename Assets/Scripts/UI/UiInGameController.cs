using System.Collections.Generic;
using System.Xml;
using Collectable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiInGameController : MonoBehaviour
    {
        public Sprite LifeUsed;
        public Sprite Life;
        public Sprite CrystalBlue;
        public Sprite CrystalGreen;
        public Sprite CrystalRed;
        public Sprite CrystalEmpty;

        private Dictionary<CrystalScript.CrystalType, Sprite> Crystals =
            new Dictionary<CrystalScript.CrystalType, Sprite>();

        private Text _coins;
        private Text _fruits;


        private void Start()
        {
            _coins = transform.Find("Coins").Find("Text").GetComponent<Text>();
            _fruits = transform.Find("Fruits").Find("Text").GetComponent<Text>();

            SetLifes(3);

            Crystals.Add(CrystalScript.CrystalType.Blue, CrystalBlue);
            Crystals.Add(CrystalScript.CrystalType.Green, CrystalGreen);
            Crystals.Add(CrystalScript.CrystalType.Red, CrystalRed);
        }

        public void SetCoins(int coins)
        {
            _coins.text = "" + coins;
        }

        public void SetFruits(int fruits, int maxFruits)
        {
            _fruits.text = fruits + "/" + maxFruits;
        }

        public void SetLifes(int life)
        {
            for (int i = 0; i < transform.Find("Lifes").childCount; i++)
            {
                transform.Find("Lifes").Find("Life" + (i + 1)).GetComponent<Image>().sprite =
                    i < life ? Life : LifeUsed;
            }
        }

        public void SetCrystal(CrystalScript.CrystalType type)
        {
            transform.Find("Crystals").Find(type.ToString()).GetComponent<Image>().sprite = Crystals[type];
        }
    }
}