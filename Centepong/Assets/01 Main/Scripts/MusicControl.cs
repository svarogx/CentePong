using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

	public AudioClip backgroundMusic;

	private AudioSource audioControl;

	// Use this for initialization
	void Awake(){
		if (!CentePong.objectMusic)
			CentePong.objectMusic = true;
		else
			Destroy (this.gameObject);

		audioControl = GetComponent<AudioSource> ();
	}

	void Start () {
		DontDestroyOnLoad (this.gameObject);	

		if (PlayerPrefs.HasKey ("sliderComments"))
			CentePong.volumeComments = PlayerPrefs.GetFloat ("sliderComments");
		else
			CentePong.volumeComments = 1.0f;
		if (PlayerPrefs.HasKey ("sliderViewers"))
			CentePong.volumeViewers = PlayerPrefs.GetFloat ("sliderViewers");
		else
			CentePong.volumeViewers = 1.0f;
		if (PlayerPrefs.HasKey ("sliderMusic"))
			CentePong.volumeMusic = PlayerPrefs.GetFloat ("sliderMusic");
		else
			CentePong.volumeMusic = 1.0f;

		audioControl.volume = CentePong.volumeMusic;
		audioControl.loop = true;
		audioControl.clip = backgroundMusic;
		audioControl.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CentePong.destroyMusic)
			Destroy (this.gameObject);
		audioControl.volume = CentePong.volumeMusic;
	}
}
