using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonificados : MonoBehaviour
{
    private Environment env;
    private RewardedAd rewardedAd;

    private void Awake() {
        env = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<Environment>();
    }

    private void Start() {
        LoadRewardedAd();
    }

    public void LoadRewardedAd() {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null) {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(env.appEnvironment.rewartAddID, adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                rewardedAd.OnAdFullScreenContentClosed += () => {
                    UIManager.NewHorde();
                };
                RegisterReloadHandler(rewardedAd);
            });
    }

    public void ShowRewardedAd() {
        const string rewardMsg =
            "Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd()) {
            rewardedAd.Show((Reward reward) => {
                PlayerPrefs.SetInt("MustHealPlayer", 1);
            });
        }
    }

    private void RegisterReloadHandler(RewardedAd ad) {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) => {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };

    }
}
