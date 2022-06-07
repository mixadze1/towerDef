using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using propertiesTower;
using TMPro;

public class TurretTowerShop : MonoBehaviour
{
    [SerializeField] private TurretTower _turret;
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
        if (PlayerPrefs.GetFloat(PrefsTurret.DAMAGE) > 3)
        {
            _turret._targetingRange = PlayerPrefs.GetFloat(PrefsTurret.RANGE);
            _turret._shootsPerSeconds = PlayerPrefs.GetFloat(PrefsTurret.DAMAGE);
        }
    }

    private void CalculateText()
    {
        if (PlayerPrefs.GetFloat(PrefsTurret.DAMAGE) > 3)
        {
            _damageText.text = PlayerPrefs.GetFloat(PrefsTurret.DAMAGE).ToString("F2");
            _rangeText.text = PlayerPrefs.GetFloat(PrefsTurret.RANGE).ToString("F2");
        }
           
        else
        {
            _damageText.text = _turret._shootsPerSeconds.ToString("F2");
            _rangeText.text = _turret._targetingRange.ToString("F2");
        }
            

        _damageTextUpgrade.text = "+ " + _upgradeDamage.ToString("F2");
        _rangeTextUpgrade.text = "+ " + _upgradeRange.ToString("F2");
    }
    public void Upgrade()
    {
        if (GUIManager.instance.Dollar > _shop.PriceUpgradeTurret)
        {
            GUIManager.instance.Dollar -= _shop.PriceUpgradeTurret;
            PlayerPrefs.SetInt(Dollar.DECIMAL, GUIManager.instance.Dollar);

            _turret._shootsPerSeconds += _upgradeDamage;
            PlayerPrefs.SetFloat(PrefsTurret.DAMAGE, _turret._shootsPerSeconds);

            _turret._targetingRange += _upgradeRange;
            PlayerPrefs.SetFloat(PrefsTurret.RANGE, _turret._targetingRange);

            Init();
        }
    }
}
