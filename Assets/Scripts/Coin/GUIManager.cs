using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _dollarText;
    private int _coin;
    private int _dollar;
    public static GUIManager instance;
    void Awake()
    {
        instance = GetComponent<GUIManager>();
        _coinText.text = _coin.ToString();
    }

   
    public int Coin
    {
        get
        {
            return _coin;
        }

        set
        {
            _coin = value;
            _coinText.text = _coin.ToString();
        }
    }
    public int Dollar
    {
        get
        {
            return _dollar;
        }
        set
        {
            _dollar = value;
            _dollarText.text = _dollar.ToString();
        }
    }
}
