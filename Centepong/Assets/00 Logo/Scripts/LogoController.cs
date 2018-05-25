using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoController : MonoBehaviour {

	public Sprite backgroundA;
	public Sprite logoA;
	public Sprite backgroundB;
	public Sprite logoB;
	public bool isOptionA = true;
	public float timeToBegin = 2.0f;
	public float timeToHold = 2.0f;
	public float timeToWait = 2.0f;
	public string nextSceneName;
	public AudioClip[] playSound;

	public Image background;
	public Image logo;
	public Animator logoAnimator;

	private AudioSource logoAudio;

	void Awake(){
		logoAudio = GetComponent<AudioSource> ();
	}

	void Start () {
		if (isOptionA) {
			background.sprite = backgroundA;
			logo.sprite = logoA;
		} else {
			background.sprite = backgroundB;
			logo.sprite = logoB;
		}
		Invoke ("LogoShow", timeToBegin);
	}

	void LogoShow(){
		logoAudio.Stop ();
		logoAudio.loop = false;
		logoAudio.clip = playSound[Random.Range(0, playSound.Length)];
		logoAudio.Play ();
		logoAnimator.SetInteger ("step", 1);
		Invoke ("LogoHide", timeToHold);
	}

	void LogoHide(){
		logoAnimator.SetInteger ("step", 2);
		Invoke ("LogoClose", timeToWait);
	}

	void LogoClose(){
		if (nextSceneName.Length > 0)
			SceneManager.LoadScene (nextSceneName);
	}
}
