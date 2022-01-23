using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4575739";
    private string interstitialID = "Interstitial_Android";
    public bool TestMode;

    void Start()
    {
        Advertisement.Initialize(gameID, TestMode);
        Advertisement.AddListener(this);
    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void ShowRewardedVideo()
    {

    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementID)
    {

    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {

    }


    public void OnUnityAdsDidError(string message)
    {
        //Show or log the error here
    }

    public void OnUnityAdsDidStart(string placementID)
    {
        //Do this if ads starts
    }

    public void GetReward()
    {
        PlayerController.flagGreenLifeCrystalIsTaken = true;
    }
}