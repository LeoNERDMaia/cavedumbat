using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonGoogleEvents : MonoBehaviour 
{
	public GameController controller;

	private dfButton _button;

	// Called by Unity just before any of the Update methods is called the first time.
	public void Start()
	{
		// Obtain a reference to the dfButton instance attached to this object
		this._button = GetComponent<dfButton>();
	}

	public void Update()
	{
		_button.IsVisible = !controller.googlePlay;
	}

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		controller.panelStartMenu.Hide();
		controller.doPlayLogin();
	}

}
