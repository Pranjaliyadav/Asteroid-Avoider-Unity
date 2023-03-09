using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Android;
using UnityEngine.iOS;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance;

    private GameOverHandler gameOverHandler;

    [SerializeField] private bool testMode = true; //we only making ads in test mode, and not real ads, it'll show a unity ads screen test

    //you can get gameId from cloud>ads>gameId in unity editor

#if UNITY_ANDROID
    private string gameId = "5198214";
#endif

#if UNITY_IOS
  private string gameId = "5198215";
#endif


    private void Awake() //making a singleton instance
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Advertisement.Initialize(gameId, testMode, this); //add this script to ad listener
        }
    }


    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this.gameOverHandler = gameOverHandler; //when we use this, that takes the one being passed in class on line 11, without this, it'll take the one being passed in fn

        Advertisement.Show("rewardedVideo", this); // after running the add it'll come to below showAds interface and check if the add completed, skipped or didint run at all
        //this rewardedVideo is the ad id we created on unity dashboard for ios and android
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed : {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Unity Ads loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads failed to load : {placementId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity Ads show failed : {placementId} - {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED: //when ads finished completely then you can continue game
                gameOverHandler.ContinuesGame();
                break;
            case UnityAdsShowCompletionState.SKIPPED: //we dont do anything 
                //ad was skipped
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }
}
