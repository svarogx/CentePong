using UnityEngine;
using System.Collections;

public class SysRotate : MonoBehaviour {

	public BallControl[] rotateObjects;
	public Vector3[] objectsPosition;
	public float normalSize;
	public Vector3 selectPosition;
	public float selectSize;
	public string saveName;
	public float timeOffset = 0.5f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < rotateObjects.Length; i++) {
			rotateObjects [i].SetInitial (i + 1, objectsPosition, selectPosition, selectSize, normalSize, timeOffset);  
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RotateEvent(int touchObjectID){
		if (touchObjectID == 1)
			return;
		int indx = touchObjectID - 1;
		foreach (BallControl objectControl in rotateObjects) {
			objectControl.SetTarget (indx);
		}
	}

	public int SaveObject(){
		for (int i = 0; i < rotateObjects.Length; i++) {
			if (rotateObjects [i].GetObjectPosition () == 1)
				return i;
		}
		return 0;
	}
}
