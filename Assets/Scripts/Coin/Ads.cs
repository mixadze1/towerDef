using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private int _coin;
    [SerializeField] private int _hp;
    public void GetCoin()
    {
        GUIManager.instance.Coin += _coin;
    }
    public void GetHP()
    {
        _game.CurrentPlayerHealth += _hp;
        _game.HealthText.text = _game.CurrentPlayerHealth.ToString();
    }
}
