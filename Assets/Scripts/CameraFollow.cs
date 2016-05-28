using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public BatMove batMove;
	public float distance = 6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!batMove.isDead)
			this.transform.position = new Vector3(batMove.gameObject.transform.position.x + distance, batMove.gameObject.transform.position.y, this.transform.position.z);
	}
}
