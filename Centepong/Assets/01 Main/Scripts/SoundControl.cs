using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundControl : MonoBehaviour {

	public Slider sliderComments;
	public Slider sliderViewers;
	public Slider sliderMusic;


	public Sprite[] backSprite; 
	public string HomeScene;

	private SpriteRenderer backImage;

	void Awake(){
		backImage = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		int indx = Random.Range(0, backSprite.Length);
		backImage.sprite = backSprite[indx];

		if (PlayerPrefs.HasKey ("sliderComments"))
			sliderComments.value = PlayerPrefs.GetFloat ("sliderComments");
		if (PlayerPrefs.HasKey ("sliderViewers"))
			sliderViewers.value = PlayerPrefs.GetFloat ("sliderViewers");
		if (PlayerPrefs.HasKey ("sliderMusic"))
			sliderMusic.value = PlayerPrefs.GetFloat ("sliderMusic");

	}

	void FixedUpdate(){
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) 
				HomeAccess();
		}
		CentePong.volumeMusic = sliderMusic.value;
	}

	public void HomeAccess(){
		if (HomeScene.Length < 1)
			return;
		PlayerPrefs.SetFloat ("sliderComments", sliderComments.value);
		CentePong.volumeComments = sliderComments.value;
		PlayerPrefs.SetFloat ("sliderViewers", sliderViewers.value);
		CentePong.volumeViewers = sliderViewers.value;
		PlayerPrefs.SetFloat ("sliderMusic", sliderMusic.value);
		CentePong.volumeMusic = sliderMusic.value;
		PlayerPrefs.Save ();
		SceneManager.LoadScene (HomeScene);
	}

}
