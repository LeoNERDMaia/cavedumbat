using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;

public class ButtonRankingEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		StartCoroutine("ShowLeaderBoard");
	}

	public void ShowLeaderBoard() {
		SocialController.GetInstance().ShowLeaderBoard();
	}

}
