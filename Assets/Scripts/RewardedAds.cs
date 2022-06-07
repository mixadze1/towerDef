using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using propertiesTower;
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button buttonShowAd;
    private string _androidAdId = "Rewarded_Android";
    private string _iosAdId = "Rewarded_iOS";
    private string _adID; 
    
    private void Awake()
    {
        _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosAdId : _androidAdId;
        LoadAd();
    }
    public void LoadAd()
    {
        Debug.Log(_adID);
        Advertisement.Load(_adID, this);
    }
    public void ShowAd()
    {
        Advertisement.Show(_adID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("startAds");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GUIManager.instance.Dollar += 50;
        PlayerPrefs.SetInt(Dollar.DECIMAL, GUIManager.instance.Dollar);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("zagruzilca");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
