using UnityEngine;
using System.Collections;

public class LoadCourt : MonoBehaviour {

	public Sprite[] courts;

	private SpriteRenderer courtRender;

	void Awake(){
		courtRender = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		if (CentePong.court < courts.Length)
			courtRender.sprite = courts [CentePong.court];
	
	}
	
}
