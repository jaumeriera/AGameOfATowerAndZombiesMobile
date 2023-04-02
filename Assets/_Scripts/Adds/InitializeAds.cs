using GoogleMobileAds.Api;
using UnityEngine;

public class InitializeAds : MonoBehaviour
{
    public void Start() {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }
}