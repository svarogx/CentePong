using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MediaLogo : MonoBehaviour {

	public string nextScene;

	public void LogoPassOut(){
		if (nextScene.Length < 1)
			return;
		SceneManager.LoadScene (nextScene);
	}

}
