using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

	private string androidAdId = "ca-app-pub-3865236618689243/8686910219";
	private string iosAdId = 	 "ca-app-pub-3865236618689243/5175040611";

	void Start () {

		string adId = androidAdId;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			adId = iosAdId;
		}
		
		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView(
			adId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}
}
