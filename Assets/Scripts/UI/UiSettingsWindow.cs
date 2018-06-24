using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiSettingsWindow : MonoBehaviour
    {
        public Sprite SoundClicked;
        public Sprite SoundNotClicked;
        public Sprite MusicClicked;
        public Sprite MusicNotClicked;

        private void Start()
        {
            transform.Find("ButtonSound").GetComponent<Image>().sprite =
                PlayerPrefs.GetInt("sound", 1) == 1 ? SoundClicked : SoundNotClicked;
            transform.Find("ButtonMusic").GetComponent<Image>().sprite =
                PlayerPrefs.GetInt("music", 1) == 1 ? MusicClicked : MusicNotClicked;
        }

        public void ClickSoundButton()
        {
            PlayerPrefs.SetInt("sound", PlayerPrefs.GetInt("sound", 1) == 1 ? 0 : 1);
            transform.Find("ButtonSound").GetComponent<Image>().sprite =
                PlayerPrefs.GetInt("sound", 1) == 1 ? SoundClicked : SoundNotClicked;
            PlayerPrefs.Save();
        }

        public void ClickMusicButton()
        {
            PlayerPrefs.SetInt("music", PlayerPrefs.GetInt("music", 1) == 1 ? 0 : 1);
            transform.Find("ButtonMusic").GetComponent<Image>().sprite =
                PlayerPrefs.GetInt("music", 1) == 1 ? MusicClicked : MusicNotClicked;
            PlayerPrefs.Save();
        }
       
    }
}