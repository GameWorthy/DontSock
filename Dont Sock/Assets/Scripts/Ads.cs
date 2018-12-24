using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {
    
    private BannerView bannerView;

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3865236618689243~5733443815";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3865236618689243~3698307417";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3865236618689243/8686910219";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3865236618689243/5175040611";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
