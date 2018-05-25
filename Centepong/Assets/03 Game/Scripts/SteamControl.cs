using UnityEngine;
using System.Collections;

public class SteamControl : MonoBehaviour {

	public float angleOffset;
	public float steamRadius;

	private Rigidbody2D ballBody;
	private BallBehaviour ballCtrl;
	private SpriteRenderer steamRender;

	// Use this for initialization
	void Awake(){
		ballBody = GetComponentInParent<Rigidbody2D> ();
		ballCtrl = GetComponentInParent<BallBehaviour> ();
		steamRender = GetComponent<SpriteRenderer> ();
	}

	void Start () {
	
	}
	
	void Update () {
	
	}

	void FixedUpdate(){
		if (ballCtrl.isStopped) {
			steamRender.enabled = false;
			return;
		}
		steamRender.enabled = true;
		float speedAngle = Vector2.Angle (new Vector2 (1.0f, 0.0f), ballBody.velocity);
//		Debug.Log (speedAngle);
		Vector3 cross = Vector3.Cross (new Vector2 (1.0f, 0.0f), ballBody.velocity);
//		Debug.Log ("Cross Z: " + cross.z);
		if (cross.z < 0) 
			speedAngle = 360 - speedAngle;
//		Debug.Log ("Angle: " + speedAngle);
		float speedRad = ((speedAngle + 180) * Mathf.PI) / 180;
		Vector3 steamPos = new Vector3 (steamRadius * Mathf.Cos (speedRad), steamRadius * Mathf.Sin (speedRad), 0.0f); 
//		Debug.Log ("Steam Position: " + steamPos); 
		Vector3 steamAngle = new Vector3 (0.0f, 0.0f, speedAngle - 90 + angleOffset);
//		Debug.Log ("Steam Angle: " + steamAngle); 
		this.transform.localPosition = steamPos;
		this.transform.eulerAngles = steamAngle;
	}
}
