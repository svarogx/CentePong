using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	#if UNITY_STANDALONE_WIN
	private BallBehaviour ballScript;

	private Vector3 iniPos;
	private Vector3 endPos;

	private float iniTime;
	private float endTime;

	void Start () {
		ballScript = GetComponent<BallBehaviour> (); 
	}

	// Update is called once per frame
	void Update () {
	}
	
    // Checks if the mouse button is pressed
	void OnMouseDown() {
//		Debug.Log ("PUMM");
		if (!ballScript.isStopped)
			return;
		iniPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
		iniTime = Time.time;
	}
	
    // Checks if the mouse button is released
	void OnMouseUp() {
		if (!ballScript.isStopped)
			return;
		endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)); 
		endTime = Time.time;
		Vector2 speed = new Vector2 ((endPos.x - iniPos.x) / (endTime - iniTime), (endPos.y - iniPos.y) / (endTime - iniTime));
		ballScript.MoveBall (speed);
	}
	#endif 
}
