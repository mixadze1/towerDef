using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class CountZombie : MonoBehaviour
{
   
    [SerializeField] private int _countZombie;
    [SerializeField] private TextMeshProUGUI _countZombieUI;
    private const string _leaderBoard = "CgkI3p_y2vgPEAIQAQ";
    private const string CORRECT_COUNT_ZOMBIE = "zombieDie";

    public static CountZombie _instance;
    private void Awake()
    {
        
        _instance = this;
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(sucess =>
        {
            if (sucess)
            {
            }
            else
            {
            }
        });
        Init();
    }

    private void Init()
    {
        if (PlayerPrefs.GetInt(CORRECT_COUNT_ZOMBIE) > 0)
            _countZombie = PlayerPrefs.GetInt(CORRECT_COUNT_ZOMBIE);

                
        else
            _countZombie = 0;

        _countZombieUI.text = _countZombie.ToString();
    }

    public void IsDieZombie()
    {
        _countZombie++;
        PlayerPrefs.SetInt(CORRECT_COUNT_ZOMBIE, _countZombie );
        Social.ReportScore(_countZombie, _leaderBoard, (bool sucess) => { });
        Init();
    }
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
