using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Disclaimer : MonoBehaviour {

	public string nextScene;
	public float waitTime;
	public GameObject loadPanel;

	private AsyncOperation ao;
	private bool loadingScene = false;



	void Start () {
		loadPanel.SetActive (false);	
		Invoke ("LogoPassOut", waitTime);
	}
	
	void FixedUpdate () {
		if (loadingScene) 
			loadPanel.GetComponent<TransitionScript> ().alpha = ao.progress; 
	}

	public void LogoPassOut(){
		if (nextScene.Length < 1)
			return;
//		SceneManager.LoadScene (nextScene);
		ao = SceneManager.LoadSceneAsync (nextScene, LoadSceneMode.Single);
		ao.allowSceneActivation = true;
		loadingScene = true;
		loadPanel.SetActive (true);
	}

}
