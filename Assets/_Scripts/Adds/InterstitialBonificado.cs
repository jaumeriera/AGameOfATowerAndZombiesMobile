using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialBonificado : MonoBehaviour
{
    private Environment env;
    private RewardedInterstitialAd rewardedInterstitialAd;

    private void Awake() {
        env = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<Environment>();
    }

    private void Start() {
        LoadRewardedInterstitialAd();
        RegisterReloadHandler(rewardedInterstitialAd);
        rewardedInterstitialAd.OnAdFullScreenContentClosed += () => {
            UIManager.NewHorde();
        };
    }

    /// <summary>
    /// Loads the rewarded interstitial ad.
    /// </summary>
    public void LoadRewardedInterstitialAd() {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null) {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(env.appEnvironment.rewartAddID, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
    }

    public void ShowRewardedInterstitialAd() {
        const string rewardMsg =
            "Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd()) {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                PlayerPrefs.SetInt("MustHealPlayer", 1);
            });
        }
    }
    private void RegisterReloadHandler(RewardedInterstitialAd ad) {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedInterstitialAd();
        };
    }
}
