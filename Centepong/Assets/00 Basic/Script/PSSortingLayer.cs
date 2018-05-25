using UnityEngine;
using System.Collections;

public class PSSortingLayer : MonoBehaviour {

	public string sortingLayerName;
	public int sortingLayer = 0;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = sortingLayerName;
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingOrder = sortingLayer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
