using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;
    private int coin;
    public static GUIManager instance;
    void Awake()
    {
        instance = GetComponent<GUIManager>();
        coinTxt.text = coin.ToString();
    }

   
    public int Coin
    {
        get
        {
            return coin;
        }

        set
        {
            coin = value;
            coinTxt.text = coin.ToString();
        }
    }

}
