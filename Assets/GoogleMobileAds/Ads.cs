using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Ads : MonoBehaviour {
    private string second_life = "ca-app-pub-4800162937668095/5639897775";
    private string death = "ca-app-pub-4800162937668095/9060268030";
    private string arrival = "ca-app-pub-4800162937668095/8001912205";

    public InterstitialAd death_ad;
    public InterstitialAd portal_ad;
    public RewardedAd second_life_ad;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private float deltaTime;

    public UnityEvent on_second_life_earned;
    public UnityEvent on_restart;
    public UnityEvent on_portal;

    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;
    public UnityEvent OnAdLeavingApplicationEvent;
    public bool showFpsMeter = true;
    public Text fpsMeter;
    public Text statusText;

    #region WAYFARER FROG
    public void restartButton() {
        if (PlayerPrefs.GetInt("no_ads") == 1) return;
        if (ShowInterstitialAd()) {

        } else {
            on_restart.Invoke();
        }
    }
    public void toPortal() {
        if (PlayerPrefs.GetInt("no_ads") == 1) return;
        if (ShowPortalAd()) {

        } else {
            on_portal.Invoke();
        }
    }
    public void showSecondLife() {
        ShowRewardedAd(second_life_ad);
    }
    #endregion

    #region UNITY MONOBEHAVIOR METHODS

    private void Start() {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);

        
        //RequestAndLoadRewardedAd(second_life_ad, second_life);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus) {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.

        MobileAdsEventExecutor.ExecuteInUpdate(() => {
            statusText.text = "Initialization complete";
            RequestAndLoadInterstitialAd();
            RequestAndLoadRewardedAd();
            RequestAndLoadPortalAd();
        });
    }

    private void Update() {
        if (showFpsMeter) {
            fpsMeter.gameObject.SetActive(true);
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsMeter.text = string.Format("{0:0.} fps", fps);
        } else {
            //fpsMeter.gameObject.SetActive(false);
        }
    }

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest() {
        return new AdRequest.Builder().Build();
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("unity-admob-sample")
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd() {
        if (PlayerPrefs.GetInt("no_ads") == 1) return;
        statusText.text = "Requesting Interstitial Ad.";
        //Debug.Log("RequestAndLoadInterstitialAd");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = death;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (death_ad != null) {
            death_ad.Destroy();
        }

        death_ad = new InterstitialAd(adUnitId);

        // Add Event Handlers
        death_ad.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        death_ad.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();

        death_ad.OnAdClosed += (sender, args) => on_restart.Invoke();
        //death_ad.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();

        death_ad.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

        // Load an interstitial ad
        death_ad.LoadAd(CreateAdRequest());
    }
    public void RequestAndLoadPortalAd() {
        if (PlayerPrefs.GetInt("no_ads") == 1) return;
        statusText.text = "Requesting Interstitial Ad.";
        //Debug.Log("RequestAndLoadInterstitialAd");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = portal;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (portal_ad != null) {
            portal_ad.Destroy();
        }

        portal_ad = new InterstitialAd(adUnitId);

        // Add Event Handlers
        portal_ad.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        portal_ad.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();

        portal_ad.OnAdClosed += (sender, args) => on_portal.Invoke();
        //death_ad.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();

        portal_ad.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

        // Load an interstitial ad
        portal_ad.LoadAd(CreateAdRequest());
    }

    public bool ShowInterstitialAd() {
        if (death_ad.IsLoaded()) {
            death_ad.Show();
        } else {
            statusText.text = "Interstitial ad is not ready yet";
        }
        return death_ad.IsLoaded();
    }
    public bool ShowPortalAd() {
        if (portal_ad.IsLoaded()) {
            death_ad.Show();
        } else {
            statusText.text = "Interstitial ad is not ready yet";
        }
        return portal_ad.IsLoaded();
    }

    public void DestroyInterstitialAd(InterstitialAd interstitialAd) {
        if (interstitialAd != null) {
            interstitialAd.Destroy();
        }
    }
    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd() {
        statusText.text = "Requesting Rewarded Ad.";
#if UNITY_EDITOR
        string adUnitId = "unused";

#elif UNITY_ANDROID
        string adUnitId = second_life;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        // create new rewarded ad instance
        second_life_ad = new RewardedAd(adUnitId);

        // Add Event Handlers
        second_life_ad.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        second_life_ad.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        second_life_ad.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        second_life_ad.OnAdFailedToShow += (sender, args) => OnAdFailedToShowEvent.Invoke();
        second_life_ad.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();

        second_life_ad.OnUserEarnedReward += (sender, args) => on_second_life_earned.Invoke();
        //else rewardedAd.OnUserEarnedReward += (sender, args) => OnUserEarnedRewardEvent.Invoke();

        // Create empty ad request
        second_life_ad.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd(RewardedAd rewardedAd) {
        if (rewardedAd != null) {
            rewardedAd.Show();
        } else {
            statusText.text = "Rewarded ad is not ready yet.";
        }
    }

    public void RequestAndLoadRewardedInterstitialAd() {
        statusText.text = "Requesting Rewarded Interstitial Ad.";
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) => {

            if (error != null) {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "RewardedInterstitialAd load failed, error: " + error;
                });
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;
            MobileAdsEventExecutor.ExecuteInUpdate(() => {
                statusText.text = "RewardedInterstitialAd loaded";
            });
            // Register for ad events.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial presented.";
                });
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial dismissed.";
                });
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial failed to present.";
                });
                this.rewardedInterstitialAd = null;
            };
        });
    }

    public void ShowRewardedInterstitialAd() {
        if (rewardedInterstitialAd != null) {
            rewardedInterstitialAd.Show((reward) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "User Rewarded: " + reward.Amount;
                });
            });
        } else {
            statusText.text = "Rewarded ad is not ready yet.";
        }
    }

    #endregion
}