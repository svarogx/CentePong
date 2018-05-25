using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainBackground : MonoBehaviour {

	public Sprite[] backSprites;

	public GameObject rainPrefab;
	public GameObject paperPrefab;
	public GameObject fireworkPrefab;


	public string playButton;
	public string optionButton;
	public string soundButton;

	public GameObject panelLoad;

	private GameObject rainPoint;
	private GameObject[] fireworkPoint; 

	private SpriteRenderer backControl;

	private AsyncOperation ao;
	private bool loadingScene = false;

	void Awake(){
		backControl = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		panelLoad.SetActive (false);

		rainPoint = GameObject.FindGameObjectWithTag ("Rain");
		fireworkPoint = GameObject.FindGameObjectsWithTag ("Firework");

		switch (Random.Range (0, 3)) {
		case 0:
			Instantiate (rainPrefab, rainPoint.transform.position, Quaternion.identity); 
			break;
		case 1:
			Instantiate (paperPrefab, rainPoint.transform.position, Quaternion.identity); 
			break;
		case 2: 
			foreach (GameObject spawnPoint in fireworkPoint) {
				Instantiate (fireworkPrefab, spawnPoint.transform.position, Quaternion.identity);
			}
			break;
		} 

		int indx = Random.Range (0, backSprites.Length);
		backControl.sprite = backSprites [indx];

		if (PlayerPrefs.HasKey ("sliderMusic"))
			CentePong.volumeMusic = PlayerPrefs.GetFloat ("sliderMusic");
		if (PlayerPrefs.HasKey ("sliderViewers"))
			CentePong.volumeViewers = PlayerPrefs.GetFloat ("sliderViewers");
		if (PlayerPrefs.HasKey ("sliderComments"))
			CentePong.volumeComments = PlayerPrefs.GetFloat ("sliderComments");
		
	}

	void Update(){
		if (loadingScene) 
			panelLoad.GetComponent<TransitionScript> ().alpha = ao.progress; 
		
	}

	void FixedUpdate(){
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey(KeyCode.Escape))
				BackAccess ();
		}
	}

	public void PlayAccess(){
		if (playButton.Length < 1)
			return;
		CentePong.playerSelection = 1;
		ao = SceneManager.LoadSceneAsync (playButton, LoadSceneMode.Single);
		ao.allowSceneActivation = true;
		loadingScene = true;
		panelLoad.SetActive (true);
	}

	public void OptionAccess(){
		if (optionButton.Length < 1)
			return;
		ao = SceneManager.LoadSceneAsync (optionButton, LoadSceneMode.Single);
		ao.allowSceneActivation = true;
		loadingScene = true;
		panelLoad.SetActive (true);
	}

	public void SoundAccess(){
		if (soundButton.Length < 1)
			return;
		SceneManager.LoadScene (soundButton);
	}

	public void BackAccess(){
		Application.Quit ();
	}
}
