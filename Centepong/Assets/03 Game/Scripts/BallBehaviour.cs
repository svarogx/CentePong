using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	public float maxDistance = 1.0f;
	public GameObject touchArea;

	private Rigidbody2D ballBody;
	private Animator ballAnimator;

	public bool isStopped;
	public float lowSpeed = 0.1f;
	public float maxSpeed = 20.0f;

	public Sprite[] balls;

	private Vector3 stopCenter;
	private Vector3 lastPosition;

	private GameControl gameControl;
	private SpriteRenderer ballRender;

	void Awake(){
		ballBody = GetComponent<Rigidbody2D> ();
		ballAnimator = GetComponent<Animator> ();
		ballRender = GetComponent<SpriteRenderer>();
		gameControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl> ();
	}


	void Start () {
		if (CentePong.ball < balls.Length)
			ballRender.sprite = balls [CentePong.ball];

		this.transform.position = Vector3.zero;
		InitialBall ();
//		MoveBall (new Vector2 (23, 19));
	}
	
	void Update () {
	
	}

	void FixedUpdate(){
		if (isStopped) {
			if (lastPosition != this.transform.position) {
				float dist = Vector3.Distance (stopCenter, this.transform.position);
				if (dist > maxDistance)
					this.transform.position = lastPosition;
				else
					lastPosition = this.transform.position; 
			}
		} else {
			if (ballBody.velocity.magnitude < lowSpeed)
				StopBall ();		
		}
	}

	private void StopBall(){
		ballBody.velocity = Vector2.zero;
		stopCenter = this.transform.position;
		lastPosition = stopCenter;
		ballAnimator.SetBool ("play", false);
		isStopped = true;
		if (touchArea != null) {
			touchArea.transform.position = stopCenter;
			touchArea.SetActive (true); 
		}
		CentePong.turnOK = true;
		CentePong.turnLeft = !CentePong.turnLeft;
	}

	public void InitialBall(){
		this.transform.position = Vector3.zero;
		StopBall ();
	}

	public void MoveBall(Vector2 speedBall){
		float factor = 1.0f;
		if (speedBall.magnitude > maxSpeed) {
			factor = maxSpeed / speedBall.magnitude;
		}
		ballBody.velocity = factor * speedBall;
		ballAnimator.SetBool ("play", true);
		isStopped = false;
		if (touchArea != null) {
			touchArea.SetActive (false); 
		}
		CentePong.turnOK = false;
	}

	void OnTriggerEnter2D(Collider2D hit){
		if (hit.gameObject.tag == "Goal") {
			if (this.transform.position.x < 0)
				gameControl.LeftGoal ();
			else
				gameControl.RightGoal ();
			this.transform.position = Vector3.zero;
			StopBall ();
		}
	}

	void OnBecameInvisible(){
		this.transform.position = Vector3.zero;
		StopBall ();
	}

	void OnCollisionEnter2D(Collision2D hit){
		if (hit.gameObject.tag == "Keeper")
			gameControl.KeeperSound ();
		if (hit.gameObject.tag == "Foot")
			gameControl.PlayerSound ();
		
		/*		if (hit.gameObject.tag == "Wall") {
			float xSpeed = 0.0f, ySpeed = 0.0f;
			switch (hit.gameObject.name) {
			case "Up":
				xSpeed = 0.9f * ballBody.velocity.x;
				ySpeed = -0.9f * ballBody.velocity.y;
				break;
			case "Down":
				xSpeed = 0.9f * ballBody.velocity.x;
				ySpeed = -0.9f * ballBody.velocity.y;
				break;
			case "Right":
				xSpeed = -0.9f * ballBody.velocity.x;
				ySpeed = 0.9f * ballBody.velocity.y;
				break;
			case "Left":
				xSpeed = -0.9f * ballBody.velocity.x;
				ySpeed = 0.9f * ballBody.velocity.y;
				break;
			}
			ballBody.velocity = new Vector2 (xSpeed, ySpeed);
		}
*/	}
}
