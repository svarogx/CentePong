using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionControl : MonoBehaviour {

	public SysRotate ballControl;
	public SysRotate courtControl;

	private SpriteRenderer backgroundControl;
	public Sprite[] backgroundImages;


	public string backMenu;


	// Use this for initialization
	void Awake(){
		backgroundControl = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		int indx = Random.Range (0, backgroundImages.Length);
		backgroundControl.sprite = backgroundImages [indx];

		Debug.Log ("Option: " + CentePong.playerSelection);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				SaveAll ();
				BackOption ();
			}
		}
	}

	public void BackOption(){
		SceneManager.LoadScene (backMenu);
	}

	public void SaveAll(){
		CentePong.ball = ballControl.SaveObject ();
		CentePong.court = courtControl.SaveObject (); 
	}
}
