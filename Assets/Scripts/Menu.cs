using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
	public void Play()
	{
		SceneManager.LoadScene ("Main");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
