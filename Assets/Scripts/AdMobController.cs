using UnityEngine;
using System.Collections;

public class AdMobController : MonoBehaviour {

	public GameController controller;

	private bool oldShowAd = true;

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
