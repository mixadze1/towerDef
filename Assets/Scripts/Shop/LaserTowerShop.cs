using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using propertiesTower;

public class LaserTowerShop : MonoBehaviour
{
    [SerializeField] private LaserTower _laser;
    [SerializeField] private Shop _shop;
    [Header("UILaser")]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _rangeText;

    [Header("UpgradeUI")]
    [SerializeField] private TextMeshProUGUI _damageTextUpgrade;
    [SerializeField] private TextMeshProUGUI _rangeTextUpgrade;

    [SerializeField, Range(1f, 10f)] private float _upgradeDamage = 2;
    [SerializeField, Range(0.05f, 0.5f)] private float _upgradeRange = 0.1f;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        CalculateText();
        CalculateTower();    
    }

    private void CalculateTower()
    {
        if (PlayerPrefs.GetFloat(PrefsLaser.LASER_RANGE) > 3)
        {
            _laser._targetingRange = PlayerPrefs.GetFloat(PrefsLaser.LASER_RANGE);
        }

        if (PlayerPrefs.GetFloat(PrefsLaser.LASER_DAMAGE) > 10)
            _laser._damagePerSecond = PlayerPrefs.GetFloat(PrefsLaser.LASER_DAMAGE);
    }

    private void CalculateText()
    {
        if (PlayerPrefs.GetFloat(PrefsLaser.LASER_DAMAGE) > _laser._damagePerSecond)
            _damageText.text = PlayerPrefs.GetFloat(PrefsLaser.LASER_DAMAGE).ToString("F2");
        else
            _damageText.text = _laser._damagePerSecond.ToString("F2");


        if (PlayerPrefs.GetFloat(PrefsLaser.LASER_RANGE) > _laser._targetingRange)
            _rangeText.text = PlayerPrefs.GetFloat(PrefsLaser.LASER_RANGE).ToString("F2");
        else
            _rangeText.text = _laser._targetingRange.ToString("F2");

        _damageTextUpgrade.text = "+ " + _upgradeDamage.ToString("F2");
        _rangeTextUpgrade.text = "+ " + _upgradeRange.ToString("F2");
    }
    public void Upgrade()
    {
        if (GUIManager.instance.Dollar > _shop.PriceUpgradeLaser)
        {
            GUIManager.instance.Dollar -= _shop.PriceUpgradeLaser;
            PlayerPrefs.SetInt(Dollar.DECIMAL, GUIManager.instance.Dollar);
            _laser._damagePerSecond += _upgradeDamage;
            PlayerPrefs.SetFloat(PrefsLaser.LASER_DAMAGE, _laser._damagePerSecond);

            _laser._targetingRange += _upgradeRange;
            PlayerPrefs.SetFloat(PrefsLaser.LASER_RANGE, _laser._targetingRange);

            Init();
        }
    }
}
