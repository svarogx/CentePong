using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSelection : MonoBehaviour {

	public Sprite[] backgroundSprites;

	public SysRotate equipSelect;
	public SysRotate formationSelect;

	public string backMenu;
	public string nextMenu;

	public GameObject warningPanel;
	public GameObject loadPanel;
	public Text gameTitle;

	private SpriteRenderer spriteControl;

	private AsyncOperation ao;
	private bool loadingScene = false;

	void Awake(){
		spriteControl = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		warningPanel.SetActive (false);
		loadPanel.SetActive (false);

		if (CentePong.playerSelection >= 1 && CentePong.playerSelection <= 2)
			gameTitle.text = "Jugador " + CentePong.playerSelection;
		else
			gameTitle.text = "";
		
		int indx = Random.Range (0, backgroundSprites.Length);
		spriteControl.sprite = backgroundSprites[indx];
	}
	
	// Update is called once per frame
	void Update () {
		if (loadingScene) 
			loadPanel.GetComponent<TransitionScript> ().alpha = ao.progress; 
	}

	void FixedUpdate(){
		if (Application.platform == RuntimePlatform.Android) 
			if (Input.GetKey (KeyCode.Escape)) 
				BackAccess ();
	}

	public void BackAccess(){
		switch (CentePong.playerSelection) {
		case 1:
			if (backMenu.Length < 1)
				return;
			SceneManager.LoadScene (backMenu);
			break;
		case 2:
			CentePong.playerSelection = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			break;
		}
	}

	public void HomeAccess(){
		if (backMenu.Length < 1)
			return;
		SceneManager.LoadScene (backMenu);
	}

	public void NextAccess(){
		switch (CentePong.playerSelection) {
		case 1:
			CentePong.player1 = equipSelect.SaveObject ();
			CentePong.formation1 = formationSelect.SaveObject ();
			CentePong.playerSelection = 2;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			break;
		case 2:
			if (nextMenu.Length < 1)
				return;
			if (CentePong.player1 == equipSelect.SaveObject ()) {
				warningPanel.SetActive (true);
				return;
			}
			CentePong.player2 = equipSelect.SaveObject ();
			CentePong.formation2 = formationSelect.SaveObject ();
			CentePong.playerSelection = 0;
			CentePong.destroyMusic = true;
//			CentePong.objectMusic = false;
//			SceneManager.LoadScene(nextMenu);
			ao = SceneManager.LoadSceneAsync (nextMenu, LoadSceneMode.Single);
			ao.allowSceneActivation = true;
			loadingScene = true;
			loadPanel.SetActive (true);
			break;
		}
	}

	public void WarningPanelOFF(){
		warningPanel.SetActive (false);
	}
}
