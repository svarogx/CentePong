using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInput : MonoBehaviour {

	public BallBehaviour ballControl;
	public float touchFactor = 0.5f;

	private int fingerCtrl = -1;
	private Vector3 speedCtrl;
	private float InitTime;

	void Start () {
	}
	
	void Update () {
	}

	void FixedUpdate(){
		TouchControl ();
	}

	private void TouchControl(){
		RaycastHit hit;
		Ray ray;
		int indx;
		foreach (Touch touch in Input.touches) {
			indx = (int)touch.fingerId;
			ray = Camera.main.ScreenPointToRay (touch.position);
			switch (touch.phase) {
			case TouchPhase.Began:
				if (Physics.Raycast (ray, out hit, 20.0f)) {
					if (hit.collider.tag == "Ball") {
						if (ballControl.isStopped) {
							speedCtrl = new Vector3 (touch.position.x, touch.position.y, 0.0f);
							InitTime = Time.time;
							fingerCtrl = indx;
						}
					}
				}
				break;
			case TouchPhase.Canceled:
				fingerCtrl = -1;
				break;
			case TouchPhase.Ended:
				if (fingerCtrl > -1 && fingerCtrl == indx) {
					if (ballControl.isStopped) {
						speedCtrl.x = (touch.position.x - speedCtrl.x) / (Time.time - InitTime);
						speedCtrl.y = (touch.position.y - speedCtrl.y) / (Time.time - InitTime);
						ballControl.MoveBall (speedCtrl * touchFactor);
					}
					fingerCtrl = -1;
				}
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				break;
			}
		}
	}

	public void ShutGame(){
		Application.Quit();
	}

}
