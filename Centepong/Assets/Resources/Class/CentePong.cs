using UnityEngine;
using System.Collections;

public class CentePong {

	static public int playerSelection = -1;
	static public int player1 = 2;
	static public int formation1 = 2;
	static public int scoreplayer1 = 0;

	static public bool turnOK = false;
	static public bool turnLeft = true;

	static public int player2 = 3;
	static public int formation2 = 3;
	static public int scoreplayer2 = 0;

	static public int court = 3;
	static public int ball = 3;

	static public float volumeComments = 1.0f;
	static public float volumeViewers = 1.0f;
	static public float volumeMusic = 1.0f;

	static public bool destroyMusic = false;
	static public bool objectMusic = false;

	static public Vector3[] formations1 = new [] {new Vector3 (-1.5f, 0.0f, 0.0f), 
		new Vector3 (-2.5f, 2.75f, 0.0f), new Vector3 (-2.5f, -2.75f, 0.0f), new Vector3 (-4.5f, 4.4f, 0.0f), 
		new Vector3 (-4.5f, -4.4f, 0.0f), new Vector3 (-6.0f, 1.1f, 0.0f), new Vector3 (-6.0f, -1.1f, 0.0f)
	};
	static public Vector3[] formations2 = new [] {new Vector3 (-1.5f, 0.0f, 0.0f), 
		new Vector3 (-2.5f, 2.75f, 0.0f), new Vector3 (-2.5f, -2.75f, 0.0f), new Vector3 (-3.5f, 0.55f, 0.0f), 
		new Vector3 (-5.0f, 2.75f, 0.0f), new Vector3 (-5.0f, -2.75f, 0.0f), new Vector3 (-6.0f, 0.0f, 0.0f)
	};
	static public Vector3[] formations3 = new [] {new Vector3 (-0.5f, -1.65f, 0.0f), 
		new Vector3 (-1.5f, 1.1f, 0.0f), new Vector3 (-3.5f, 0.0f, 0.0f), new Vector3 (-5.0f, 3.3f, 0.0f), 
		new Vector3 (-5.0f, -3.3f, 0.0f), new Vector3 (-6.0f, 1.1f, 0.0f), new Vector3 (-6.0f, -1.1f, 0.0f)
	};
	static public Vector3[] formations4 = new [] {new Vector3 (-0.5f, 3.3f, 0.0f), 
		new Vector3 (-0.5f, -3.3f, 0.0f), new Vector3 (-2.5f, 1.65f, 0.0f), new Vector3 (-2.5f, -1.65f, 0.0f), 
		new Vector3 (-5.0f, 3.3f, 0.0f), new Vector3 (-5.0f, -3.3f, 0.0f), new Vector3 (-6.0f, 0.0f, 0.0f)
	};
	
}
