using UnityEngine;
using UnityEngine.Events;

public class InterstitialIron : MonoBehaviour
{
    
    public event UnityAction GrandUpdate;
    
    public static InterstitialIron Instance;
    
#if UNITY_ANDROID
    private string appKey = "1c1f82b8d";
#elif UNITY_IPHONE
    string appKey = "";
#else
string appKey = "unexpected_playform"
#endif

    private void Start()
    {
        Instance = this;
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey);
    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SDKInitialized;


        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdReadyEvent += RewardedVideoOnAdReadyEvent;
        IronSourceRewardedVideoEvents.onAdLoadFailedEvent += RewardedVideoOnAdLoadFailedEvent;
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    private void SDKInitialized()
    {
        print("SDK is init");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        IronSource.Agent.onApplicationPause(pauseStatus);
    }


    #region Rewarded

    public void LoadRewarded()
    {
        IronSource.Agent.loadRewardedVideo();
    }

    public void ShowRewarded()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Not_Ready");
        }
    }

    /************* RewardedVideo AdInfo Delegates *************/
// Indicates that the Rewarded video ad was loaded successfully. 
// AdInfo parameter includes information about the loaded ad
    void RewardedVideoOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
    }

// Indicates that the Rewarded video ad failed to be loaded 
    void RewardedVideoOnAdLoadFailedEvent(IronSourceError error)
    {
    }

// The Rewarded Video ad view has opened. Your activity will loose focus. 
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }

// The Rewarded Video ad view is about to be closed. Your activity will regain its focus. 
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
    }

// The user completed to watch the video, and should be rewarded. 
// The placement parameter will include the reward data. 
// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        GrandUpdate.Invoke();
    }

// The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
    }

// Invoked when the video ad was clicked. 
// This callback is not supported by all networks, and we recommend using it only if  
// it's supported by all networks you included in your build. 
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
    }

    #endregion
}