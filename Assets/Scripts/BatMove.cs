using UnityEngine;
using System.Collections;

public class BatMove : MonoBehaviour {

	public GameController controller;
	public Animator batAnimator;
	public GameObject explosionPrefab;

	public float headDown;
	public float headUp;
	public float maxAngle = 60f;
	public float maxInclination = 60;
	public float startSpeed = 5;
	public float minSpeed = 5;
	public float maxSpeed = 10;
	public float speed = 0;

	public float angle = 0;
	public bool isFlapping = false;

	public bool isDead = false;

	public bool firstTap = true;

	private Vector3 startPosition;
	private bool wasStopped = false;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
	}

	void FixedUpdate () {
		switch (controller.status) {
		case GameController.ControllerStatus.started: this.DoMove(); break;
		case GameController.ControllerStatus.ending: this.DoEnding();break;
		case GameController.ControllerStatus.stopped: this.DoStopped(); break;
		}
	}

	private void DoStopped() {
		rigidbody2D.gravityScale = 0;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0.0f;
		this.transform.position = startPosition;
		this.transform.rotation = Quaternion.identity;
		batAnimator.SetInteger("State", 0);
		angle = 0;
		isDead = false;
		firstTap = true;
		wasStopped = true;
	}

	private void DoMove() {
		if (!wasStopped)
			DoStopped();
		int currentState = 0;

		if (!isDead && !firstTap) {
			isFlapping = ((Input.GetAxis("Fire1") != 0) || (Input.touchCount > 0));
			if (isFlapping)
				currentState = 1;
			else
				currentState = 0;
		
			if (isFlapping)
				angle += headUp;
			else
				angle += headDown;
		
			if (angle > maxAngle)
				angle = maxAngle;
			if (angle < (maxAngle * -1))
				angle = maxAngle * -1;
		
			float currentAngle = Mathf.DeltaAngle(0, this.transform.rotation.eulerAngles.z);
		
			if ((currentAngle >= maxAngle / -3) && (currentAngle <= maxAngle / 3))
				speed = Mathf.Lerp(speed, startSpeed, 2f * Time.deltaTime);
			else {
				if (currentAngle < maxAngle / 3) {
					speed = Mathf.Lerp(speed, maxSpeed, 1f * Time.deltaTime);
					if (isFlapping)
						currentState = 3;
					else
						currentState = 4;
				}
				if (currentAngle > maxAngle / -3) {
					speed = Mathf.Lerp(speed, minSpeed, 2f * Time.deltaTime);
					if (isFlapping)
						currentState = 2;
					else
						currentState = 5;
				}
			}
			if (speed < minSpeed)
				speed = minSpeed;

			if ((currentAngle <= maxInclination) && (currentAngle >= (maxInclination * -1)))
				this.transform.Rotate(0, 0, angle * Time.fixedDeltaTime);
			else {
				if (currentAngle > maxInclination)
					this.transform.Rotate(0, 0, headDown * Time.fixedDeltaTime);
				if (currentAngle < (maxInclination * -1))
					this.transform.Rotate(0, 0, headUp * Time.fixedDeltaTime);
			}

			this.transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
			controller.distance = (int)(this.transform.position.x + 20);
		} else {
			if (isDead)
				currentState = 6;
			firstTap = !((Input.GetAxis("Fire1") != 0) || (Input.touchCount > 0));
			controller.showTutorial = firstTap;
		}
		
		batAnimator.SetInteger("State", currentState);
	}

	private void DoEnding() {
		firstTap = true;
		batAnimator.SetInteger("State", 6);
		wasStopped = false;
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		if ((collision.gameObject.tag == "Border") && !isDead) {
			this.isDead = true;
			speed = 0.0f;
			rigidbody2D.gravityScale = 20;
			rigidbody2D.AddForce(new Vector2(1000, 0));
			controller.EndGame();
			GameObject explosion = (GameObject)Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
			Destroy(explosion, 1.0f);
		}
	}
}
