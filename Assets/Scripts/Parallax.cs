using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public float parallaxSize = 30.72f;
	public BatMove batMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float batAngle = Mathf.Deg2Rad * Mathf.DeltaAngle(0, batMove.gameObject.transform.rotation.eulerAngles.z);
		float x = Mathf.Cos(batAngle) * batMove.speed * -1;
		float y = Mathf.Sin(batAngle) * batMove.speed * -1;
		//Debug.Log("Speed " + batMove.speed + "; Angle " + batAngle + "; x " + x + "; y " + y);
		this.transform.Translate(x / 10 * Time.deltaTime, y / 10 * Time.deltaTime, 0);
		if (this.transform.localPosition.x < parallaxSize)
			this.transform.localPosition = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z);
		if ((this.transform.localPosition.y < parallaxSize) || ((this.transform.localPosition.y > (parallaxSize / -3))))
			this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
	}
}
