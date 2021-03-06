using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContentPrice : MonoBehaviour
{
    [SerializeField] private TilesBuilder _tilesBuilder;
    [SerializeField] private TextMeshProUGUI _priceMortar;
    [SerializeField] private TextMeshProUGUI _priceLaser;
    [SerializeField] private TextMeshProUGUI _priceWall;
    [SerializeField] private TextMeshProUGUI _priceTurret;
    [SerializeField] private TextMeshProUGUI _priceTurretTypeTwo;
    [SerializeField] private TextMeshProUGUI _priceElecroMortar;
    public void Initialize()
    {
        _priceMortar.text = _tilesBuilder.PriceMortar.ToString();
        _priceLaser.text = _tilesBuilder.PriceLaser.ToString();
        _priceWall.text = _tilesBuilder.PriceWall.ToString();
        _priceTurret.text = _tilesBuilder.PriceTurret.ToString();
        _priceElecroMortar.text = _tilesBuilder.PriceElectro.ToString();
        _priceTurretTypeTwo.text = _tilesBuilder.PriceTurretTypeTwo.ToString();
    }
}
