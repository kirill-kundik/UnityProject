using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class UiStartGame : MonoBehaviour {

		public void LoadScene()
		{
			SceneManager.LoadScene("Level1");
		}
	}
}
