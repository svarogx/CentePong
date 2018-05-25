using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

	public BallBehaviour ballPlayer;
	public AudioSource audioComments;
	public AudioSource audioViewers;
	public AudioSource audioMusic;

	public AudioClip[] audioTerms;
	public AudioClip[] audioGoal;
	public AudioClip[] audioKeeper;
	public AudioClip[] audioRandom;
	public AudioClip audioBackground;

	public GameObject pausePanel;
	public GameObject intervalPanel;
	public GameObject finalPanel;
	public GameObject loadPanel;

	public Text termText;
	public Text timerText;

	public int minutesTerm = 2;

	public string mainMenu;
	public string actualMenu;

	private int leftScore = 0;
	private int rightScore = 0;

	private bool pausedGame = false;
	private bool pausedComments = false;
	private bool pausedViewers = false;

	private int gameStatus = -1;
	private int gameMinutes = 0;
	private int gameSeconds = 0;
	private bool gameClock = false;

	private int playerTouch = 4;

	private AsyncOperation ao;
	private bool loadingScene = false;

	private GameInput inputControl;

	void Awake(){
		inputControl = GetComponent<GameInput> ();
	}

	// Use this for initialization
	void Start () {
		loadPanel.SetActive (false);

		audioComments.loop = false;
		audioComments.volume = CentePong.volumeComments;
		audioViewers.loop = false;
		audioViewers.volume = CentePong.volumeViewers;
		audioMusic.loop = true;
		audioMusic.volume = CentePong.volumeMusic;
		audioMusic.clip = audioBackground;

		FirstTerm ();
		Invoke("RandomSound", Random.Range(5.0f, 10.0f)); 
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if ((gameStatus % 2) == 0) {
				if (pausedGame)
					ResumeGame ();
				else
					PauseGame ();
			}
		}
		if (loadingScene) 
			loadPanel.GetComponent<TransitionScript> ().alpha = ao.progress; 
	}

	public void LeftGoal(){
		rightScore += 1;
		CentePong.scoreplayer2 = rightScore;
		ballPlayer.InitialBall ();
		CentePong.turnLeft = false;
		audioComments.clip = audioGoal[Random.Range(0, audioGoal.Length)];
		audioComments.Play ();
	}

	public void RightGoal(){
		leftScore += 1;
		CentePong.scoreplayer1 = leftScore;
		ballPlayer.InitialBall ();
		CentePong.turnLeft = true;
		audioComments.clip = audioGoal[Random.Range(0, audioGoal.Length)];
		audioComments.Play ();
	}

	public void KeeperSound(){
		if (audioComments.isPlaying)
			return;
		audioComments.clip = audioKeeper[Random.Range(0, audioKeeper.Length)];
		audioComments.Play ();
	}

	public void PlayerSound(){
		if (audioComments.isPlaying)
			return;
		playerTouch -= 1;
		if (playerTouch != 0)
			return;
		audioComments.clip = audioRandom[Random.Range(0, audioRandom.Length)];
		audioComments.Play ();
		playerTouch = 4;
	}

	public void RandomSound(){
		Invoke("RandomSound", Random.Range(5.0f, 10.0f)); 
		if (audioComments.isPlaying)
			return;
		if ((gameStatus % 2) == 1)
			return;
		audioComments.clip = audioRandom[Random.Range(0, audioRandom.Length)];
		audioComments.Play ();
	}

	public void FirstTerm(){
		gameStatus = 0;
		termText.text = "Primer Tiempo";
		rightScore = 0;
		CentePong.scoreplayer2 = rightScore;
		leftScore = 0;
		CentePong.scoreplayer1 = leftScore;
		audioViewers.clip = audioTerms [0];
		CentePong.turnLeft = true;
		CommonTerm ();
	}

	public void SecondTerm(){
		gameStatus = 2;
		termText.text = "Segundo Tiempo";
		audioViewers.clip = audioTerms [2];
		CentePong.turnLeft = false;
		CommonTerm ();
	}

	private void CommonTerm(){
		gameMinutes = minutesTerm;
		gameSeconds = 0;
		gameClock = true;
		pausePanel.SetActive (false);
		intervalPanel.SetActive (false);
		finalPanel.SetActive (false);
		inputControl.enabled = true;
		audioMusic.Pause ();
		audioViewers.Play ();
		ballPlayer.InitialBall ();
		InvokeRepeating ("SecondToSecond", 0.1f, 1.0f);
	}

	public void ResumeGame(){
		inputControl.enabled = true;
		Time.timeScale = 1.0f;
		pausedGame = false;
		pausePanel.SetActive (false);
		audioMusic.Pause ();
		if (pausedComments) {
			audioComments.Play ();
			pausedComments = false;
		}
		if (pausedViewers) {
			audioViewers.Play ();
			pausedViewers = false;
		}
	}

	public void HomeAccess(){
		if (mainMenu.Length < 1)
			return;
		Time.timeScale = 1.0f;
//		SceneManager.LoadScene (mainMenu);
		CentePong.destroyMusic = false;
		CentePong.objectMusic = false;
		ao = SceneManager.LoadSceneAsync (mainMenu, LoadSceneMode.Single);
		ao.allowSceneActivation = true;
		loadingScene = true;
		loadPanel.SetActive (true);
	}

	private void SecondToSecond(){
		switch (gameStatus) {
		case 0:
			break;
		case 1:
			inputControl.enabled = false;
			termText.text = "Intermedio";
			gameClock = false;
			audioComments.Stop ();
			audioViewers.Stop ();
			audioMusic.Play ();
			intervalPanel.SetActive (true);
			CancelInvoke ();
			break;
		case 2: 
			break;
		case 3:
			inputControl.enabled = false;
			termText.text = "Final";
			gameClock = false;
			audioComments.Stop ();
			audioViewers.Stop ();
			audioMusic.Play ();
			finalPanel.SetActive (true);
			CancelInvoke ();
			break;
		default:
			return;
		}
		if (gameClock) {
			if (gameSeconds < 10)
				timerText.text = "0" + gameMinutes.ToString () + ":0" + gameSeconds.ToString ();
			else
				timerText.text = "0" + gameMinutes.ToString () + ":" + gameSeconds.ToString ();
			gameSeconds -= 1;
			if (gameSeconds < 0) {
				gameSeconds = 59;
				gameMinutes -= 1;
			}
			if (gameMinutes < 0)
				gameStatus += 1;
			if (!audioComments.isPlaying) {
				if (gameMinutes == 1 && gameSeconds == 0) {
					audioComments.clip = audioTerms[1];
					audioComments.Play ();
				}
				if (gameMinutes == 0 && gameSeconds == 2) {
					audioComments.clip = audioTerms[3];
					audioComments.Play ();
				}
			}
		}
	}

	private void PauseGame(){
		Time.timeScale = 0.0f;
		inputControl.enabled = false;
		pausedGame = true;
		pausePanel.SetActive (true);
		if (audioComments.isPlaying) {
			audioComments.Pause ();
			pausedComments = true;
		}
		if (audioViewers.isPlaying) {
			audioViewers.Pause ();
			pausedViewers = true;
		}
		audioMusic.Play ();
	}

}
