using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
public class Ads : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId = "4782924";
    [SerializeField] private string _iosGameId = "4782925";
    [SerializeField] private bool _testMode = true;
    private string _gameId;
    void Awake()
    {
        InitializeAds();
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialization");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
       Debug.Log($"Unity Ads failed: {error.ToString()} - {message}");
    }
}
