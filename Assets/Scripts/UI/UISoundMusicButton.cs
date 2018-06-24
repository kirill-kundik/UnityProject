using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UISoundMusicButton : MonoBehaviour
	{

		public Sprite Clicked;
		public Sprite NotClicked;
		public string ButtonName;
	
		private bool _isClicked = false;

		public void Click()
		{
			if (_isClicked)
			{
				transform.Find(ButtonName).GetComponent<Image>().sprite = NotClicked;
			}
			else
			{
				transform.Find(ButtonName).GetComponent<Image>().sprite = Clicked;			
			}
		}

	}
}
