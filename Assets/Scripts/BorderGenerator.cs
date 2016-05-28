using UnityEngine;
using System.Collections;

public class BorderGenerator : MonoBehaviour {

	public GameObject borderDownObjectPrefab;
	public GameObject borderUpObjectPrefab;
	public Queue bordersDown = new Queue();
	public Queue bordersUp = new Queue();

	public int maxBordersAlive = 20;

	public bool generateMore = false;
	public float lastAngle = 0.0f;
	public float lastX = 0.0f;
	public float lastY = 0.0f;
	public float distanceY = 8.0f;
	public float minimunDistanceY = 2.5f;
	public float noiseAngle = 30.0f;
	public float maxAngle = 60.0f;

	public float startDistanceY;

	// Use this for initialization
	void Start () {
		startDistanceY = distanceY;
		for (int i = 0; i < maxBordersAlive - 1; i++)
			GenerateOne();
	}

	public void Restart() {
		lastAngle = 0.0f;
		lastX = -0.0f;
		lastY = 0.0f;
		distanceY = startDistanceY;

		for (int i = 0; i < maxBordersAlive - 1; i++)
			GenerateOne();
	}

	private void GenerateOne() {
		float x = lastX;
		float y = lastY;

		float minNoise = noiseAngle * -1;
		float maxNoise = noiseAngle;

		if (lastAngle > maxNoise / 0.5f) {
			maxNoise = 0;
			minNoise = minNoise / 3;
		}

		if (lastAngle < minNoise / 0.5f) {
			maxNoise = maxNoise / 3;
			minNoise = 0;
		}

		//Debug.Log(minNoise + " - " + maxNoise);

		lastAngle += Random.Range(minNoise, maxNoise);
		lastAngle = Mathf.Clamp(lastAngle, maxAngle * -1, maxAngle);
		
		GameObject border;
		if (bordersDown.Count >= maxBordersAlive) {
			border = (GameObject)bordersDown.Dequeue();
			border.transform.position = new Vector3(x, y, 10);
		} else
			border = (GameObject)GameObject.Instantiate(borderDownObjectPrefab, new Vector3(x, y, 10), Quaternion.identity);
		RandomBorder scriptRandomBorder = (RandomBorder)border.GetComponent(typeof(RandomBorder));
		scriptRandomBorder.angle = lastAngle;
		scriptRandomBorder.ApplyAngle();
		bordersDown.Enqueue(border);
		
		lastX += scriptRandomBorder.adjacentSide / scriptRandomBorder.hypotenuse * 1.28f;
		lastY += scriptRandomBorder.opositeSide / scriptRandomBorder.hypotenuse * 1.28f;
		
		if (bordersUp.Count >= maxBordersAlive) {
			border = (GameObject)bordersUp.Dequeue();
			border.transform.position = new Vector3(x, y + distanceY, 10);
		} else
			border = (GameObject)GameObject.Instantiate(borderUpObjectPrefab, new Vector3(x, y + distanceY, 10), Quaternion.identity);
		scriptRandomBorder = (RandomBorder)border.GetComponent(typeof(RandomBorder));
		scriptRandomBorder.angle = lastAngle;
		scriptRandomBorder.ApplyAngle();
		bordersUp.Enqueue(border);
		if (distanceY > minimunDistanceY)
			distanceY -= 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (generateMore) {
			GenerateOne();
			generateMore = false;
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		generateMore = (collision.gameObject.tag == "Controller");
	}
}















