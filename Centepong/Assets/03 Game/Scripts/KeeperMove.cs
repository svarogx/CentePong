using UnityEngine;
using System.Collections;

public class KeeperMove : MonoBehaviour {

	public float speedThrottle = 0.3f;
	public Vector3 CenterPosition;
	public bool isLeft = true;
	public float arcHeigh = 1.0f;

	private GameObject ballObject;
	private Vector3 target;
	// Use this for initialization
	void Start () {
		ballObject = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (isLeft) {
			if (ballObject.transform.position.x < 0)
				target = new Vector3 (CenterPosition.x, Mathf.Clamp (ballObject.transform.position.y, -arcHeigh, arcHeigh), 0.0f);
			else
				target = CenterPosition; 
		} else {
			if (ballObject.transform.position.x > 0) 
				target = new Vector3 (CenterPosition.x, Mathf.Clamp (ballObject.transform.position.y, -arcHeigh, arcHeigh), 0.0f);
			else 
				target = CenterPosition; 
		}
	}

	void FixedUpdate(){
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * speedThrottle); 

	}
}
