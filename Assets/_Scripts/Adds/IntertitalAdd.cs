using GoogleMobileAds.Api;
using UnityEngine;

public class IntertitalAdd : MonoBehaviour
{

    private InterstitialAd interstitialAd;
    private Environment env;

    private void Awake() {
        env = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<Environment>();
    }

    private void Start() {
        LoadInterstitialAd();
        RegisterReloadHandler(interstitialAd);
    }

    public void LoadInterstitialAd() {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null) {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest.Builder()
              .AddKeyword("unity-admob")
              .Build();

        InterstitialAd.Load(env.appEnvironment.intertitialAddID, adRequest,
          (InterstitialAd ad, LoadAdError error) => {
              // if error is not null, the load request failed.
              if (error != null || ad == null) {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitialAd = ad;
          });

    }

    public void ShowInterstitial() {
        interstitialAd.Show();
    }

    private void RegisterReloadHandler(InterstitialAd ad) {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }
}
