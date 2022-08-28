using System.Collections;
using UnityEngine;


//using GoogleMobileAds.Api;

using UnityEngine.Advertisements;

public class Banner : MonoBehaviour
{

    // Coogle AdMob
    //    private BannerView bannerView;

    //    public void Awake()
    //    {
    //        // Initialize the Google Mobile Ads SDK.
    //        MobileAds.Initialize(initStatus => { });

    //        RequestBanner();
    //    }

    //    private void RequestBanner()
    //    {
    //#if UNITY_ANDROID
    //            string adUnitId = "ca-app-pub-6706604023850336/4247389049";
    //#elif UNITY_IPHONE
    //            string adUnitId = "";
    //#else
    //        string adUnitId = "unexpected_platform";
    //#endif

    //        // Create a 320x50 banner at the top of the screen.
    //        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

    //        // Create an empty ad request.
    //        AdRequest request = new AdRequest.Builder().Build();

    //        // Load the banner with the request.
    //        bannerView.LoadAd(request);

    //        StartCoroutine(ShowBanner());
    //    }

    //    IEnumerator ShowBanner()
    //    {        
    //        yield return new WaitForSeconds(0.5f);
    //        bannerView.Show();
    //    }




    // Unity Ads

    private string gameId = "3809647";
    private string placementId = "Banner";
    private bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBanner());
    }

    IEnumerator ShowBanner()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show(placementId);
    }
}
