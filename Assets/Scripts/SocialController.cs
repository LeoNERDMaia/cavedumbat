using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class SocialController {

	protected int totalDistance;
	protected int games;
	protected bool superation;

	private static SocialController instance = null;

	private static void StartAndroid() {
		instance = new SocialControllerAndroid();
	}

	private static void StartiOS() {
		instance = new SocialController();
	}

	public static SocialController GetInstance() {
		if (instance == null) {
			StartAndroid();
		}
		return instance;
	}

	public bool loggedIn = false;
	public int score {get;set;}
	public int highScore {get;set;}

	public virtual void DoStartingEvents() {
	}

	public bool Login() {
		try {
		Social.localUser.Authenticate((bool success) => {
			loggedIn = success;
		});
		} catch {
			loggedIn = false;
		}

		return loggedIn;
	}

	public virtual bool isStartAllowed() {
		return true;
	}

	public virtual void LogOut() {
	}

	public void DoUpdateEndGameStatus(int distance) {
		highScore = PlayerPrefs.GetInt("HighScore");
		totalDistance = PlayerPrefs.GetInt("TotalDistance");
		games = PlayerPrefs.GetInt("PlayedGames");
		superation = false;
		games ++;

		score = distance;

		if (score > highScore) {
			superation = (highScore > 0);
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
		}

		totalDistance += score;
		PlayerPrefs.SetInt("TotalDistance", totalDistance);
		PlayerPrefs.SetInt("PlayedGames", games);
		PlayerPrefs.Save();
	}

	public virtual void ComputeSocial() {
		// Achievement Superation
		if (superation)
			Social.ReportProgress("cavebatsuperation", 100.0f, (bool success) => { });

		// Achievement Enter the Cave
		if (games > 0)
			Social.ReportProgress("cavebatenterthecave", 100.0f, (bool success) => { });
		
		// Achievement 10 Games played
		if (games >= 10)
			Social.ReportProgress("cavebatplay10games", 100.0f, (bool success) => { });
		
		// Achievement First One Hundered
		if (score > 100)
			Social.ReportProgress("cavebatfirstonehundered", 100.0f, (bool success) => {	});
		
		// Achievement Five Hundered
		if (score > 500)
			Social.ReportProgress("cavebatfivehundered", 100.0f, (bool success) => {	});
		
		// Leaderboard Main Score
		Social.ReportScore(score, "cavebatmainscore", (bool success) => { });
		
		// Leaderboard Total Distance
		Social.ReportScore(totalDistance, "cavebattotaldistance", (bool success) => { });
	}

	public void ShowLeaderBoard() {
		Social.ShowLeaderboardUI();
	}

}
