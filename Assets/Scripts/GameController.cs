using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;

public class GameController : MonoBehaviour {
	public enum ControllerStatus {stopped, starting, started, ending, ended};

	public BorderGenerator generator;

	public dfPanel panelStartMenu;
	public dfPanel panelResults;
	public dfPanel panelQuit;
	public dfPanel panelGooglePlay;
	public dfLabel labelDistance;
	public dfLabel labelScore;
	public dfLabel labelHighScore;
	public dfSprite spriteTutorial;

	public int distance = 0;
	public ControllerStatus status = ControllerStatus.stopped;

	public bool showTutorial = false;
	public bool showBanner = true;
	public bool quit = false;
	public bool googlePlay = false;

	private float timeGameOver;

	private SocialController socialController;

	// Use this for initialization
	void Start () {
		socialController = SocialController.GetInstance();
		panelGooglePlay.Show();
	}

	public void doPlayLogin() {
		socialController.DoStartingEvents();
		StartCoroutine("ShowMainMenu");
	}

	public void doNotPlayLogin() {
		googlePlay = false;
		panelStartMenu.Show();
	}

	private IEnumerator ShowMainMenu() {
		yield return new WaitForSeconds(1.0f);
		bool logged = socialController.Login();
		Debug.Log(logged);
		while (!socialController.isStartAllowed())
			yield return new WaitForSeconds(0.5f);
		panelStartMenu.Show();
		googlePlay = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !quit) {
			if (status == GameController.ControllerStatus.started)
				this.DoEnded();
			else
				this.panelQuit.Show();
		}

		switch (status) {
		case GameController.ControllerStatus.stopped: this.DoStopped(); break;
		case GameController.ControllerStatus.starting: this.DoStarting(); break;
		case GameController.ControllerStatus.started: this.DoStarted(); break;
		case GameController.ControllerStatus.ending: this.DoEnding();break;
		case GameController.ControllerStatus.ended: this.DoEnded();break;
		}
		labelDistance.Text = distance.ToString();
		spriteTutorial.IsVisible = showTutorial;
	}

	public void StartGame() {
		status = ControllerStatus.starting;
	}

	public void EndGame() {
		timeGameOver = Time.time;
		status = ControllerStatus.ending;
	}

	public void StopGame() {
		status = ControllerStatus.ended;
	}

	public void ReplayGame() {
		distance = 0;
		generator.Restart();
		status = ControllerStatus.starting;
	}

	private void DoStopped() {
	}

	private void DoStarting() {
		panelQuit.Hide();
		showTutorial = true;
		status = ControllerStatus.started;
	}

	private void DoStarted() {
		if (!showTutorial && showBanner) {
			showBanner = false;
		}
	}

	private void DoEnding() {
		showBanner = true;
		if ((Time.time > timeGameOver + 2) && !panelResults.IsVisible) {
			audio.Play();
			panelResults.Show();

			socialController.DoUpdateEndGameStatus(distance);
			if (googlePlay)
				socialController.ComputeSocial();

			int highScore = socialController.highScore;
			int score = socialController.score;

			labelScore.Text = score.ToString();
			labelHighScore.Text = highScore.ToString();
		}
	}

	private void DoEnded() {
		showBanner = true;
		distance = 0;
		generator.Restart();
		panelStartMenu.Show();
		status = ControllerStatus.stopped;
		showTutorial = false;
	}
}
