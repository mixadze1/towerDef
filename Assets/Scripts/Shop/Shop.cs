using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
   [SerializeField] private List<Transform> _shopItem;
    [SerializeField] private TextMeshProUGUI _laserPrice;
    [SerializeField] private TextMeshProUGUI _mortarPrice;
    [SerializeField] private TextMeshProUGUI _turretPrice;
    [SerializeField] private TextMeshProUGUI _turretTwo;
    [SerializeField] private TextMeshProUGUI _electro;
    public int PriceUpgradeTurret;
    public int PriceUpgradeMortar;
    public int PriceUpgradeLaser;
    public int PriceUpgradeTurretTypeTwo;
    public int PriceElectro;
    private int i = 0;

    private void Awake()
    {
        InitShop();
    }
    public void InitShop()
    {
        _laserPrice.text = PriceUpgradeLaser.ToString();
        _mortarPrice.text = PriceUpgradeMortar.ToString();
        _turretPrice.text = PriceUpgradeTurret.ToString();
        _turretTwo.text = PriceUpgradeTurretTypeTwo.ToString();
        _electro.text = PriceElectro.ToString();
    }

    public void LeftButton()
    {        
        try
        {
            _shopItem[i - 1].gameObject.SetActive(true);
            _shopItem[i].gameObject.SetActive(false);
            i--;
        }
        catch 
        {
            return;
        }
            
    }
    public void RightButton()
    {
        try
        {
            _shopItem[i + 1].gameObject.SetActive(true);
            _shopItem[i].gameObject.SetActive(false);
            i++;
        }
        catch
        {
            return;
        }
    }
}
