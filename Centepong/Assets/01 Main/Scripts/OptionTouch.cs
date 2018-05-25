using UnityEngine;
using System.Collections;

public class OptionTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		Ray ray;
		BallControl casual;
		foreach (Touch touch in Input.touches) {
			ray = Camera.main.ScreenPointToRay (touch.position);
			switch (touch.phase) {
			case TouchPhase.Began:
				if (Physics.Raycast (ray, out hit, 20.0f)) {
					if (hit.collider.tag == "RotateObject") {
						casual = hit.collider.gameObject.GetComponent<BallControl> ();
						casual.OnTouchObject ();
					}
				}
				break;
			case TouchPhase.Canceled:
				break;
			case TouchPhase.Ended:
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				break;
			}
		}
	}

}

