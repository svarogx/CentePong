using UnityEngine;
using System.Collections;

public class LoadTeam : MonoBehaviour {

	public int playerID;
	public Vector3 keeperUbication;
	public GameObject playerPrefab;
	public GameObject keeperPrefab;
	public float speedThrottle;
	public float arcHeigh;

	private int playerTeam;
	private int playerFormation;
	private Vector3 keeperPosition;
	private Vector3[] playersPosition;
	private int playerFactor; 
	private string playerCountry;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		switch (playerID) {
		case 1:
			playerTeam = CentePong.player1;
			playerFormation = CentePong.formation1;
			playerFactor = 1;
			break;
		case 2:
			playerTeam = CentePong.player2;
			playerFormation = CentePong.formation2;
			playerFactor = -1;
			break;
		default:
			return;
		}
		keeperPosition = playerFactor * keeperUbication;
		switch (playerTeam) {
		case 0:
			playerCountry = "Argentina";
			break;
		case 1:
			playerCountry = "Brasil";
			break;
		case 2:
			playerCountry = "Chile";
			break;
		case 3:
			playerCountry = "Colombia";
			break;
		default:
			return;
		}
		switch (playerFormation) {
		case 0:
			playersPosition = CentePong.formations1;
			break;
		case 1:
			playersPosition = CentePong.formations2;
			break;
		case 2:
			playersPosition = CentePong.formations3;
			break;
		case 3:
			playersPosition = CentePong.formations4;
			break;
		default:
			return;
		}
		CreateKeeper ();
		for (int i = 0; i < playersPosition.Length; i++) {
			GameObject playerTemp = Instantiate (playerPrefab, playerFactor * playersPosition [i], Quaternion.identity) as GameObject;
			string playerPath = playerCountry + "/player0" + (i + 1).ToString();
			Sprite playerRender = Resources.Load<Sprite> (playerPath);
			playerTemp.GetComponent<SpriteRenderer> ().sprite = playerRender;
			playerTemp.transform.parent = this.gameObject.transform;
		}
	}
	
	private void CreateKeeper(){
		GameObject keeperObject = Instantiate(keeperPrefab, keeperPosition, Quaternion.identity) as GameObject;
		string keeperPath = playerCountry + "/keeper";
		Sprite keeperRender = Resources.Load<Sprite> (keeperPath);
		keeperObject.GetComponent<KeeperMove> ().speedThrottle = speedThrottle;
		keeperObject.GetComponent<KeeperMove> ().CenterPosition = keeperPosition;
		if (playerFactor > 0)
			keeperObject.GetComponent<KeeperMove> ().isLeft = true;
		else
			keeperObject.GetComponent<KeeperMove> ().isLeft = false;
		keeperObject.GetComponent<KeeperMove> ().arcHeigh = arcHeigh;
		keeperObject.GetComponent<SpriteRenderer> ().sprite = keeperRender;
		keeperObject.transform.parent = this.gameObject.transform;
	}

}
