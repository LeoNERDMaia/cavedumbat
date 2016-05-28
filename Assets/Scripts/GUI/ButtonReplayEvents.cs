using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonReplayEvents : MonoBehaviour 
{
	public GameController controller;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		controller.ReplayGame();
	}

}
