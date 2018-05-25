using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionScript : MonoBehaviour {

	public Image logo;
	public Text loadText;
	public float alpha = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color hey;
		hey.a = alpha;
		hey.r = 255;
		hey.g = 255;
		hey.b = 255;
		logo.color = hey;

		int i = Mathf.RoundToInt ((100 * (alpha/0.90f)));
		loadText.text = "Cargando " + i.ToString () + "%";
	}


}
