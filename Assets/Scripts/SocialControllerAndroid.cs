using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;

public class SocialControllerAndroid : SocialController {

	public override void DoStartingEvents ()
	{
		base.DoStartingEvents ();
		PlayGamesPlatform.Activate();
	}

	public override bool isStartAllowed() {
		return ((PlayGamesPlatform) Social.Active).IsAuthenticated();
	}

	public override void LogOut() {
		((PlayGamesPlatform) Social.Active).SignOut();
	}

	public override void ComputeSocial() {
		// Achievement Superation
		if (superation)
			Social.ReportProgress("CgkI7anV9I8ZEAIQBQ", 100.0f, (bool success) => { });

		// Achievement Enter the Cave
		if (games > 0)
			Social.ReportProgress("CgkI7anV9I8ZEAIQAg", 100.0f, (bool success) => { });
		
		// Achievement 10 Games played
		if (games >= 10)
			Social.ReportProgress("CgkI7anV9I8ZEAIQAw", 100.0f, (bool success) => { });
		
		// Achievement First One Hundered
		if (score > 100)
			Social.ReportProgress("CgkI7anV9I8ZEAIQBA", 100.0f, (bool success) => {	});
		
		// Achievement Five Hundered
		if (score > 500)
			Social.ReportProgress("CgkI7anV9I8ZEAIQBg", 100.0f, (bool success) => {	});
		
		// Leaderboard Main Score
		Social.ReportScore(score, "CgkI7anV9I8ZEAIQAQ", (bool success) => { });
		
		// Leaderboard Total Distance
		Social.ReportScore(totalDistance, "CgkI7anV9I8ZEAIQBw", (bool success) => { });
	}

}
