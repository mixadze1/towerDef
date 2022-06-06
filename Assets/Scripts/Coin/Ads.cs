using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
public class Ads : MonoBehaviour, IUnityAdsShowListener
{
    //"4782924";
    [SerializeField] private Game _game;
    [SerializeField] private int _coin;
    [SerializeField] private int _hp;
    void Awake()
    {
        //Advertisement.AddListener(this);
    }
    public void GetCoin()
    {
        GUIManager.instance.Coin += _coin;
    }
    public void GetHP()
    {
        _game.CurrentPlayerHealth += _hp;
        _game.HealthText.text = _game.CurrentPlayerHealth.ToString();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    { 
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }
}
