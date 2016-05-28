using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonYesPlayEvents : MonoBehaviour 
{

	public GameController controller;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		controller.doPlayLogin();
	}

}
