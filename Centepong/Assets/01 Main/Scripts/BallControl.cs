using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	private SysRotate rotateControl;

	private int nowPosition = -1;
	private int nextPosition = -1;
	private Vector3[] vectorPosition;
	private float smallSize;
	private Vector3 selectPosition;
	private float largeSize;

	private int onTransition = 0;

	private Vector3 initVector;
	private Vector3 goalVector;
	private float initTime;
	private float factor = 0.5f;

	// Use this for initialization
	void Awake(){
		rotateControl = this.transform.parent.GetComponent<SysRotate> ();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (onTransition > 0) {
			switch (onTransition) {
			case 1:
				if (nowPosition == 1) {
					transform.localScale = Vector3.Lerp (initVector, goalVector, (Time.time - initTime)/factor);
					transform.position = Vector3.Lerp (selectPosition, vectorPosition [0], (Time.time - initTime)/factor);
				}
				if ((Time.time - initTime) >= factor) {
					initVector = vectorPosition [nowPosition - 1];
					if (nowPosition == 1)
						goalVector = vectorPosition [vectorPosition.Length - 1];
					else
						goalVector = vectorPosition [nowPosition - 2];
					onTransition = 2;
					initTime = Time.time;
				}
				break;
			case 2:
				transform.position = Vector3.Lerp (initVector, goalVector, (Time.time - initTime)/factor);
				if ((Time.time - initTime) >= factor) {
					if (nowPosition == 1)
						nowPosition = 4;
					else
						nowPosition -= 1;
					if (nowPosition == nextPosition) {
						onTransition = 3;
						if (nowPosition == 1) {
							goalVector = new Vector3 (largeSize, largeSize, 1);
							initVector = new Vector3 (smallSize, smallSize, 1);
							initTime = Time.time;
						}
					} else {
						initVector = vectorPosition [nowPosition - 1];
						if (nowPosition == 1)
							goalVector = vectorPosition [vectorPosition.Length - 1];
						else
							goalVector = vectorPosition [nowPosition - 2];
						initTime = Time.time;
					}
				}
				break;
			case 3:
				if (nowPosition == 1) {
					transform.localScale = Vector3.Lerp (initVector, goalVector, (Time.time - initTime)/factor);
					transform.position = Vector3.Lerp (vectorPosition [0], selectPosition, (Time.time - initTime)/factor);
				}
				if ((Time.time - initTime) >= factor) {
					onTransition = 0;
				}
				break;
			} 
		}
	}

	public void SetInitial(int setPosition, Vector3[] vectorPos, Vector3 selectPos, float sizeL, float sizeS, float timefactor){
		nowPosition = setPosition;
		vectorPosition = vectorPos;
		smallSize = sizeS;
		selectPosition = selectPos;
		largeSize = sizeL;
		factor = timefactor;
		if (nowPosition == 1) {
			transform.localScale = new Vector3 (largeSize, largeSize, 1);
			transform.position = selectPosition;
		} else {
			transform.localScale = new Vector3 (smallSize, smallSize, 1);
			transform.position = vectorPosition[nowPosition - 1];
		}
	}

	public void SetTarget(int offsetTarget){
		if ((nowPosition - offsetTarget) <= 0)
			nextPosition = vectorPosition.Length - (offsetTarget - nowPosition);
		else
			nextPosition = nowPosition - offsetTarget;
		if (nowPosition == 1) {
			goalVector = new Vector3 (smallSize, smallSize, 1);
			initVector = new Vector3 (largeSize, largeSize, 1);
		}
		onTransition = 1;
		initTime = Time.time;
	}

	public void OnTouchObject(){
		if (nowPosition == 1 || onTransition > 0)
			return;
		rotateControl.RotateEvent (nowPosition);
	}

	public int GetObjectPosition(){
		return nowPosition;		
	}

	#if UNITY_STANDALONE_WIN
	void OnMouseDown(){
		OnTouchObject ();
	}
	#endif
}
