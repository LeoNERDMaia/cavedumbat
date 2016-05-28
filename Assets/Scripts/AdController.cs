using UnityEngine;
using System.Collections;

public class AdController : MonoBehaviour {

	public GameController controller;

	private bool oldShowAd = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (oldShowAd != controller.showBanner) {
			oldShowAd = controller.showBanner;
			if (oldShowAd)
				AdMobPlugin.ShowBannerView();
			else
				AdMobPlugin.HideBannerView();
		}
	}
}
