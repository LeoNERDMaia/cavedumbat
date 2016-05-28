using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ButtonYesEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		StartCoroutine("LogOut");
		Application.Quit();
	}

	private IEnumerator LogOut() {
		SocialController.GetInstance().LogOut();
		while (SocialController.GetInstance().isStartAllowed())
			yield return new WaitForSeconds(0.25f);
		Application.Quit();
	}

}
