using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserTowerShop : MonoBehaviour
{
    [SerializeField] private LaserTower _laser;
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
        _damageText.text = _laser._damagePerSecond.ToString("F2");
        _rangeText.text = _laser._targetingRange.ToString("F2");

        _damageTextUpgrade.text = "+ " +_upgradeDamage.ToString("F2");
        _rangeTextUpgrade.text = "+ " + _upgradeRange.ToString("F2");
    }
    public void Upgrade()
    {

        _laser._damagePerSecond += _upgradeDamage;
        _laser._targetingRange += _upgradeRange;
        Init();

    }
}
