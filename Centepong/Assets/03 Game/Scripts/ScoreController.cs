using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public Text scoreText;
	public Image scoreImage;
	public bool isLeft;

	public Sprite[] flags;

	private Animator sideAnim;

	void Awake(){
		sideAnim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		if (isLeft) {
			scoreImage.sprite = flags[CentePong.player1];
			CentePong.scoreplayer1 = 0;
		} else {
			scoreImage.sprite = flags[CentePong.player2];
			CentePong.scoreplayer2 = 0;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLeft)
			scoreText.text = CentePong.scoreplayer1.ToString();
		else
			scoreText.text = CentePong.scoreplayer2.ToString();
		if (CentePong.turnOK) {
			if (isLeft == CentePong.turnLeft)
				sideAnim.SetBool ("turnSide", true);
			else
				sideAnim.SetBool ("turnSide", false);
		} else
			sideAnim.SetBool ("turnSide", false);
	}
}
