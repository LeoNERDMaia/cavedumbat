using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonPlayEvents : MonoBehaviour 
{
	public GameController controller;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		controller.StartGame();
	}

}
