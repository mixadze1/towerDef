using UnityEngine;
using UnityEngine.Advertisements;
public class InterstialAds   : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
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
        Time.timeScale = 0;
        Debug.Log("startAds");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GUIManager.instance.Coin += 300;
        Time.timeScale = 1;
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
