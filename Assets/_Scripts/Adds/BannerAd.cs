using GoogleMobileAds.Api;
using UnityEngine;

public class BannerAd : MonoBehaviour
{
    [SerializeField]
    AdPosition position;
    [SerializeField]
    int height;
    [SerializeField]
    int width;

    BannerView _bannerView;

    private Environment env;
    AdSize size;

    private void Awake() {
        env = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<Environment>();
    }

    private void Start() {
        size = new AdSize(width, height);
        print(size);
        LoadAd();
    }

    private void OnDestroy() {
        DestroyAd();
    }

    public void CreateBannerView() {

        // If we already have a banner, destroy the old one.
        if (_bannerView != null) {
            DestroyAd();
        }

        // Create a 320x50 banner at the screen
        _bannerView = new BannerView(env.appEnvironment.bannerAddID, size, position);
    }
    public void LoadAd() {
        // create an instance of a banner view first.
        if (_bannerView == null) {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-banner")
            .Build();

        // send the request to load the ad.
        _bannerView.LoadAd(adRequest);
    }
    public void DestroyAd() {
        if (_bannerView != null) {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

}
